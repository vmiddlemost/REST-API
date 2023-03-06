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
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDto());

            return items;
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        // add "ActionResult<Item>" as a return type to Method so that it may return multiple types 
        // e.g. when item is null, allow Method to return null rather than an item id
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }
    }
}