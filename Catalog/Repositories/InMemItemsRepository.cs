using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Entities;

namespace Catalog.Repositories
{
    

    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id= Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow},
            new Item { Id= Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow},
            new Item { Id= Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow}
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
            return items.Where(item => item.Id == id).SingleOrDefault();
        }

        public void CreateItem(Item item)
        {
            items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            // Find index of the item first
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
        }

        public void DeleteItem(Guid Id)
        {
            // Same code - more or less - for updating item but for removing rather than adding
            var index = items.FindIndex(existingItem => existingItem.Id == Id);
            items.RemoveAt(index);
        }
    }
}