using Infrastructure.Abstracts;

namespace Infrastructure.Database.Models;

public sealed class CategoryEntity : DbEntity<int>
{
    public required string Name { get; set; }
}