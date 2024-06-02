using Infrastructure.Abstracts;

namespace Infrastructure.Database.Models;

public class ProductEntity: DbEntity<Guid>
{
    public required string Name { get; set; }
    public CategoryEntity Category { get; set; }
}