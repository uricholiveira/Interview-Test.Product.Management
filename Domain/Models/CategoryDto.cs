namespace Domain.Models;

public record CategoryDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public record CategoryFilterDto
{
    public string? Name { get; init; }
}

public record CreateCategoryDto
{
    public required string Name { get; init; }
}

public record UpdateCategoryDto
{
    public required string Name { get; init; }
}