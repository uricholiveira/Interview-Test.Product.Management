using System.Diagnostics.CodeAnalysis;

namespace Domain.Models;

public record ProductDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required CategoryDto Category { get; init; }
    public required InventoryDto Inventory { get; init; }
    public required DateTime CreatedAt { get; init; }
}

public record ProductFilterDto
{
    public string? Name { get; init; }
}

public record CreateProductDto
{
    public required string Name { get; init; }
    public required int CategoryId { get; init; }
}

public record UpdateProductDto
{
    public UpdateProductDto()
    {
    }

    [SetsRequiredMembers]
    public UpdateProductDto(string name, int categoryId)
    {
        Name = name;
        CategoryId = categoryId;
    }

    public required string Name { get; init; }
    public required int CategoryId { get; init; }
}