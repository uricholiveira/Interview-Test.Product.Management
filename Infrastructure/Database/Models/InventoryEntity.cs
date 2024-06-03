using Infrastructure.Abstracts;

namespace Infrastructure.Database.Models;

public sealed class InventoryEntity : DbEntity<Guid>
{
    public required Guid ProductId { get; set; }
    public ProductEntity Product { get; set; } = null!;
    public required int Quantity { get; set; }

    public IEnumerable<InventoryTransactionEntity> Transactions { get; set; } = new List<InventoryTransactionEntity>();
}