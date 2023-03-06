using System;
using Catalog.Repositories;
using Catalog.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        private readonly InMemItemsRepository repository;

        public ItemsController()
        {
            repository = new InMemItemsRepository();
        }

        // GET /items
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = repository.GetItems();
            return items;
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        // add "ActionResult<Item>" as a return type to Method so that it may return multiple types 
        // e.g. when item is null, allow Method to return null rather than an item id
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }

            return item;
        }
    }
}