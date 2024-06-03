using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Tests.Helpers;

public class Seeder
{
    private readonly DatabaseContext _databaseContext;

    public Seeder(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<ProductEntity> CreateProduct(CreateProductDto? data = null)
    {
        var category = data?.CategoryId != null
            ? await _databaseContext.Categories.FirstOrDefaultAsync(x => x.Id == data.CategoryId)
            : await _databaseContext.Categories.FirstOrDefaultAsync();

        var product = new ProductEntity
        {
            Name = data?.Name ?? "Default Product Name",
            Id = default,
            Category = category!,
            Inventory = new InventoryEntity
            {
                Id = default,
                ProductId = default,
                Quantity = 0
            }
        };

        await _databaseContext.Products.AddAsync(product);
        await _databaseContext.SaveChangesAsync();

        return product;
    }

    public async Task<CategoryEntity> CreateCategory(CreateCategoryDto? data = null)
    {
        var category = new CategoryEntity
        {
            Name = data?.Name ?? "Default Category Name",
            Id = default
        };

        await _databaseContext.Categories.AddAsync(category);
        await _databaseContext.SaveChangesAsync();

        return category;
    }
}