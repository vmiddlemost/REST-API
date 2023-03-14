using System;
using Catalog.Repositories;
using Catalog.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Catalog.Dtos;

namespace Catalog.Controllers
{
    // mark this class as an API Controller - brings in some default behaviours for the contoller class (life = easier)
    [ApiController]
    // define the route
    [Route("items")]
    // crate ItemsController class that inherits from ControllerBase 
    public class ItemsController : ControllerBase
    {
        // create readonly class as it's not going to be modified after construction
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        // GET /items
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync())
                        .Select(item => item.AsDto());

            return items;
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        // add "ActionResult<Item>" as a return type to Method so that it may return multiple types 
        // e.g. when item is null, allow Method to return null rather than an item id
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        // POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new {id = item.Id}, item.AsDto());
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        // No type included in ActionResult as PUT won't return anything
        public async Task<ActionResult> UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await repository.GetItemAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            // "with" expression -> taking "existingItem" and creating a copy of it with new modified properties (Name and Price in this case)
            // Allows me to modify properties even when they're immutable (immutable because of the "init;" keyword)
            Item updatedItem = existingItem with 
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var existingItem = await repository.GetItemAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            await repository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}