using Infrastructure.Abstracts;

namespace Infrastructure.Database.Models;

public class CategoryEntity: DbEntity<int>
{
    public required string Name { get; set; }
}