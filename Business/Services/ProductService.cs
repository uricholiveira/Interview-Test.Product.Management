using Business.Interfaces;
using Domain.Dtos;
using Infrastructure.Database;
using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly DatabaseContext _databaseContext;

    public ProductService(ILogger<ProductService> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    public async Task<ProductDto?> Get(Guid id, CancellationToken cancellationToken)
    {
        var productEntity = await _databaseContext.Products.FindAsync([id], cancellationToken);
        if (productEntity == null)
        {
            _logger.LogInformation($"Product with id {id} not found");
            return null;
        }

        var productDto = new ProductDto { Id = productEntity.Id, Name = productEntity.Name };

        return productDto;
    }

    public async Task<IEnumerable<ProductDto?>> Get(CancellationToken cancellationToken)
    {
        var productEntities = await _databaseContext.Products.ToListAsync(cancellationToken);
        var productDtos = 
            productEntities.Select(entity => new ProductDto { Id = entity.Id, Name = entity.Name });

        return productDtos;
    }

    public async Task<ProductDto?> Create(ProductDto data, CancellationToken cancellationToken)
    {
        var productEntity = new ProductEntity { Id = data.Id, Name = data.Name };
        _databaseContext.Products.Add(productEntity);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        var productDto = new ProductDto { Id = productEntity.Id, Name = productEntity.Name };
        return productDto;
    }

    public async Task<ProductDto?> Update(Guid id, ProductDto data, CancellationToken cancellationToken)
    {
        var productEntity = await _databaseContext.Products.FindAsync([id], cancellationToken);
        if (productEntity == null)
        {
            _logger.LogInformation($"Product with id {id} not found");
            return null;
        }

        productEntity.Name = data.Name;
        await _databaseContext.SaveChangesAsync(cancellationToken);

        var productDto = new ProductDto { Id = productEntity.Id, Name = productEntity.Name };
        return productDto;
    }

    public async Task<ProductDto?> Delete(Guid id, CancellationToken cancellationToken)
    {
        var productEntity = await _databaseContext.Products.FindAsync([id], cancellationToken);
        if (productEntity == null)
        {
            _logger.LogInformation($"Product with id {id} not found");
            return null;
        }

        _databaseContext.Products.Remove(productEntity);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        var productDto = new ProductDto { Id = productEntity.Id, Name = productEntity.Name };
        return productDto;
    }
}