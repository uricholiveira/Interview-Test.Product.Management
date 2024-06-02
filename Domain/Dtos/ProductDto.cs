namespace Domain.Dtos;

public record ProductDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
};