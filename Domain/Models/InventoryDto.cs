using Domain.Enums;

namespace Domain.Models;

public class InventoryDto
{
    public required Guid Id { get; init; }
    public required ProductDto Product { get; init; }
    public required int Quantity { get; init; }
}

public record InventoryFilterDto
{
    public Guid? ProductId { get; init; }
}

public record CreateInventoryDto
{
    public required Guid ProductId { get; init; }
}

public record UpdateInventoryDto
{
    public required int Quantity { get; init; }
    public required EInventoryTransactionMovementType MovementMovementType { get; set; }
}