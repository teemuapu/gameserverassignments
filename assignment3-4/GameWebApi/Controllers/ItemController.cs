using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameWebApi
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IRepository _repo;

        public ItemController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("/players/{playerId}/items/create")]
        public Task<Item> CreateItem(Guid id, NewItem item)
        {
            Item newItem = new Item()
            {
                Level = item.Level,
                Type = item.Type,
                CreationDate = DateTime.Now,
                ItemId = Guid.NewGuid()
            };
            return _repo.CreateItem(id, newItem);
        }
        [HttpGet("/players/{playerId}/items/get/{itemid}")]
        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            return _repo.GetItem(playerId, itemId);
        }
        [HttpGet("/players/{playerId}/items/all")]
        public Task<Item[]> GetAllItems(Guid playerId)
        {
            return _repo.GetAllItems(playerId);
        }
        [HttpPut("/players/{playerId}/items/modify/{itemId}")]
        public Task<Item> UpdateItem(Guid playerId, Guid itemId, ModifiedItem modifiedItem)
        {
            Item newItem = new Item()
            {
                ItemId = itemId,
                Level = modifiedItem.Level,
                Type = modifiedItem.Type,
                CreationDate = DateTime.Now
            };

            return _repo.UpdateItem(playerId, newItem);
        }
        [HttpDelete("/players/{playerId}/items/delete/{itemId}")]
        public Task<Item> DeleteItem(Guid playerId, Guid itemId)
        {
            return _repo.DeleteItem(playerId, itemId);
        }

    }
}
