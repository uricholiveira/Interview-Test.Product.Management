using Infrastructure.Abstracts;

namespace Infrastructure.Database.Models;

public sealed class ProductEntity : DbEntity<Guid>
{
    public required string Name { get; set; }
    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; }
    public InventoryEntity Inventory { get; set; }
}