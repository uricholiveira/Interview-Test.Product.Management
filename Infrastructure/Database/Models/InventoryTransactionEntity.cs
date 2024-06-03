using Domain.Enums;
using Infrastructure.Abstracts;

namespace Infrastructure.Database.Models;

public sealed class InventoryTransactionEntity : DbEntity<Guid>
{
    public required Guid InventoryId { get; set; }
    public InventoryEntity Inventory { get; set; } = null!;
    public EInventoryTransactionMovementType MovementMovementType { get; set; }
    public required int Quantity { get; set; }
}