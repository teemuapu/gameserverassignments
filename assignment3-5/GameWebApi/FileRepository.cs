using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace GameWebApi
{
    public class FileRepository : IRepository
    {
        public Task<Player> Get(Guid id)
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                Player player = JsonConvert.DeserializeObject<Player>(lines[i]);

                if (player.Id == id)
                {
                    Console.WriteLine("löytyl");
                    return Task.FromResult(player);

                }
            }
            Console.WriteLine("ei");
            return null;
        }
        public Task<Player[]> GetAll()
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            Player[] players = new Player[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                Player player = JsonConvert.DeserializeObject<Player>(lines[i]);


                players[i] = player;
            }
            return Task.FromResult<Player[]>(players);
        }
        public Task<Player> Create(Player player)
        {
            using (StreamWriter sw = File.AppendText("game-dev.txt"))
            {
                string json = JsonConvert.SerializeObject(player);
                sw.WriteLine(json);
            }

            return Task.FromResult<Player>(player);
        }
        public Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            Player[] players = new Player[lines.Length];
            Player oldPlayer = new Player();

            for (int i = 0; i < lines.Length; i++)
            {

                oldPlayer = JsonConvert.DeserializeObject<Player>(lines[i]);
                if (oldPlayer.Id == id)
                {
                    oldPlayer.Score = player.Score;
                    break;

                }
            }
            return Task.FromResult(oldPlayer);
        }
        public Task<Player> Delete(Guid id)
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            List<string> newLines = new List<string>();
            Player[] players = new Player[lines.Length];
            Player oldPlayer = new Player();
            for (int i = 0; i < lines.Length; i++)
            {

                oldPlayer = JsonConvert.DeserializeObject<Player>(lines[i]);
                if (oldPlayer.Id == id)
                {
                    continue;
                }
                else
                {
                    newLines.Add(lines[i]);
                }


            }
            File.WriteAllLines("game-dev.txt", newLines.ToArray());
            return Task.FromResult(oldPlayer);
        }
        public Task<Item> CreateItem(Guid playerid, Item item)
        {

            string[] lines = File.ReadAllLines(@"game-dev.txt");
            List<string> newLines = new List<string>();
            Player player = new Player();

            //Rewrite all the lines and modify the one line with new data

            for (int i = 0; i < lines.Length; i++)
            {
                player = JsonConvert.DeserializeObject<Player>(lines[i]);

                if (player.Id == playerid)
                {
                    player.PlayerItems.Add(item);
                }

                newLines.Add(JsonConvert.SerializeObject(player));
            }

            File.WriteAllLines("game-dev.txt", newLines.ToArray());
            return Task.FromResult(item);
        }

        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            Player player = new Player();

            for (int i = 0; i < lines.Length; i++)
            {
                player = JsonConvert.DeserializeObject<Player>(lines[i]);

                if (player.Id == playerId)
                {
                    for (int j = 0; j < player.PlayerItems.Count; j++)
                    {
                        if (player.PlayerItems.ElementAt(j).ItemId == itemId)
                        {
                            return Task.FromResult(player.PlayerItems.ElementAt(j));
                        }
                    }
                }
            }
            return null;
        }

        public Task<Item[]> GetAllItems(Guid playerId)
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            Player player = new Player();

            for (int i = 0; i > lines.Length; i++)
            {
                player = JsonConvert.DeserializeObject<Player>(lines[i]);

                if (player.Id == playerId)
                {
                    return Task.FromResult(player.PlayerItems.ToArray());
                }
            }
            return null;
        }
        public Task<Item> UpdateItem(Guid playerId, Item item)
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            List<string> newLines = new List<string>();
            Player player = new Player();
            Item modifiedItem = new Item();

            for (int i = 0; i < lines.Length; i++)
            {
                player = JsonConvert.DeserializeObject<Player>(lines[i]);

                if (player.Id == playerId)
                {
                    for (int j = 0; j < player.PlayerItems.Count; j++)
                    {
                        if (player.PlayerItems.ElementAt(j).ItemId == item.ItemId)
                        {
                            player.PlayerItems.ElementAt(j).Level = item.Level;
                            modifiedItem = player.PlayerItems.ElementAt(j);
                        }
                    }
                }

                newLines.Add(JsonConvert.SerializeObject(player));
            }
            File.WriteAllLines("game-dev.txt", newLines.ToArray());
            return Task.FromResult(modifiedItem);
        }
        public Task<Item> DeleteItem(Guid playerId, Guid itemId)
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            List<string> newLines = new List<string>();
            Player player = new Player();
            Item deletedItem = new Item();

            for (int i = 0; i < lines.Length; i++)
            {
                player = JsonConvert.DeserializeObject<Player>(lines[i]);

                if (player.Id == playerId)
                {
                    for (int j = 0; j < player.PlayerItems.Count; j++)
                    {
                        if (player.PlayerItems.ElementAt(j).ItemId == itemId)
                        {
                            // player.PlayerItems.ElementAt(j).Level = item.Level;
                            deletedItem = player.PlayerItems.ElementAt(j);
                            player.PlayerItems.Remove(deletedItem);
                        }
                    }
                }

                newLines.Add(JsonConvert.SerializeObject(player));
            }
            File.WriteAllLines("game-dev.txt", newLines.ToArray());
            return Task.FromResult(deletedItem);
        }

        public Task<List<Player>> GetPlayersMinScore(int minScore)
        {
            throw new NotImplementedException();
        }

        public Task<List<Player>> GetPlayersByItemListSize(int amountOfItems)
        {
            throw new NotImplementedException();
        }

        public Task<Player> UpdatePlayerName(Guid id, string name)
        {
            throw new NotImplementedException();
        }

        public Task<Player> CreateItemQuery(Guid playerid, Item item)
        {
            throw new NotImplementedException();
        }

        public Task<Player> CreateItemQuery(Guid playerid, NewItem newItem)
        {
            throw new NotImplementedException();
        }
    }
}

