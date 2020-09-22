using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GameWebApi
{
    public class MongoDbRepository : IRepository
    {
        private readonly IMongoCollection<Player> _laattapisteCollection;
        private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;

        public MongoDbRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("game");
            _laattapisteCollection = database.GetCollection<Player>("players");
            _bsonDocumentCollection = database.GetCollection<BsonDocument>("players");
        }
        public async Task<Player> Create(Player player)
        {
            await _laattapisteCollection.InsertOneAsync(player);


            return player;
        }

        public async Task<Item> CreateItem(Guid playerid, Item item)
        {

            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerid);
            var add = Builders<Player>.Update.AddToSet("PlayerItems", item);

            if (!_laattapisteCollection.Find(filter).Any())
            {
                throw new NotFoundException();
            }
            else
            {
                Player player = await _laattapisteCollection.FindOneAndUpdateAsync(filter, add);
                return item;
            }
        }

        public async Task<Player> Delete(Guid id)
        {
            var filter = Builders<Player>.Filter.Eq(player => player.Id, id);
            return await _laattapisteCollection.FindOneAndDeleteAsync(filter);
        }

        public async Task<Item> DeleteItem(Guid playerId, Guid itemId)
        {
            Item item = await GetItem(playerId, itemId);
            var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
            var delete = Builders<Player>.Update.PullFilter(p => p.PlayerItems, i => i.ItemId == itemId);
            await _laattapisteCollection.UpdateOneAsync(filter, delete);
            return item;

        }

        public async Task<Player> Get(Guid id)
        {
            var filter = Builders<Player>.Filter.Eq("Id", id);
            return await _laattapisteCollection.Find(filter).FirstAsync();
        }

        public async Task<Player[]> GetAll()
        {
            var players = await _laattapisteCollection.Find(new BsonDocument()).ToListAsync();
            return players.ToArray();
        }

        public async Task<Item[]> GetAllItems(Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            Player player = await _laattapisteCollection.Find(filter).FirstAsync();
            return player.PlayerItems.ToArray();
        }

        public async Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            Player player = await _laattapisteCollection.Find(filter).FirstAsync();
            for (int i = 0; i < player.PlayerItems.Count; i++)
            {
                if (player.PlayerItems[i].ItemId == itemId)
                {
                    return player.PlayerItems[i];
                }
            }
            return null;
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            Player playerModify = await Get(id);
            playerModify.Score = player.Score;
            var filter = Builders<Player>.Filter.Eq("Id", playerModify.Id);
            await _laattapisteCollection.ReplaceOneAsync(filter, playerModify);
            return playerModify;
        }

        public async Task<Item> UpdateItem(Guid playerId, Item item)
        {
            var filter = Builders<Player>.Filter.Where(p => p.Id == playerId && p.PlayerItems.Any(i => i.ItemId == item.ItemId));
            var update = Builders<Player>.Update.Set(u => u.PlayerItems[-1].Level, item.Level);

            await _laattapisteCollection.FindOneAndUpdateAsync(filter, update);

            Item getItem = await GetItem(playerId, item.ItemId);
            return getItem;
        }
    }
}
