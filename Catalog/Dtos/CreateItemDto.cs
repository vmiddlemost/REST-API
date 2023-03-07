using System.ComponentModel.DataAnnotations;

namespace Catalog.Dtos
{
    public record CreateItemDto
    {
        // add [Required] so that Name and Price are absolutely needed to POST
        [Required]
        public string Name { get; init; }
        [Required]
        // [Range(1, 1000)] = Price may only be (and including) 1 and 1000 in value
        [Range(1, 1000)]
        public decimal Price { get; init; }
    }
}