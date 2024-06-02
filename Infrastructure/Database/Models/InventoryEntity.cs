using Infrastructure.Abstracts;

namespace Infrastructure.Database.Models;

public class InventoryEntity: DbEntity<Guid>
{
    public required string Name { get; set; }
}