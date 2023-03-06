using System;

namespace Catalog.Entities
{
    // create a record rather than a class because records have "Value-based Equality"
    public record Item
    {
        // using a GUID for "Id" as it can store extremely large numbers (128-bit)
        // also use "init;" instead of "set;" so that the properties are immutable (non-modifiable) but can still be modified during construction
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}