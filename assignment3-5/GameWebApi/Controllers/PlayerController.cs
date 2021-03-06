using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameWebApi
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IRepository _repo;
        public PlayersController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("/players/{id}")]
        public async Task<Player> Get(String id)
        {
            Guid kekkonen = Guid.Parse(id);
            return await _repo.Get(kekkonen);
        }
        [HttpGet("/players/all")]
        public async Task<Player[]> GetAll()
        {
            return await _repo.GetAll();
        }
        [HttpPost("/players/create")]
        public async Task<Player> Create(NewPlayer player)
        {
            Player player1 = new Player();
            player1.Name = player.Name;
            player1.Id = Guid.NewGuid();
            player1.CreationTime = DateTime.Today;
            player1.PlayerItems = new List<Item>();
            return await _repo.Create(player1);

        }
        [HttpPut("/players/modify/{id}")]
        public async Task<Player> Modify(String id, ModifiedPlayer player)
        {
            Guid kekkonen = Guid.Parse(id);
            return await _repo.Modify(kekkonen, player);
        }
        [HttpDelete("/players/delete/{id}")]
        public async Task<Player> Delete(String id)
        {
            Guid kekkonen = Guid.Parse(id);
            return await _repo.Delete(kekkonen);
        }
        [HttpGet("/players")]
        public Task<List<Player>> GetPlayersMinScore([FromQuery] int minScore)
        {
            return _repo.GetPlayersMinScore(minScore);
        }
        [HttpPut("/players/update/{id}")]
        public Task<Player> UpdatePlayerName(Guid id, [FromQuery] string name)
        {
            return _repo.UpdatePlayerName(id, name);
        }

        [HttpGet("/players")]
        public Task<List<Player>> GetPlayersByItemListSize([FromQuery] int amountOfItems)
        {
            return _repo.GetPlayersByItemListSize(amountOfItems);
        }
        [HttpPost("/players/{id}/items/createquery")]
        public Task<Player> CreateItemQuery(Guid playerid, NewItem newItem)
        {
            return _repo.CreateItemQuery(playerid, newItem);
        }
    }
}

