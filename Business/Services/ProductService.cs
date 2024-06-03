using AutoMapper;
using Business.Interfaces;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public class ProductService : IProductService
{
    private readonly DatabaseContext _databaseContext;
    private readonly ILogger<ProductService> _logger;
    private readonly IMapper _mapper;

    public ProductService(ILogger<ProductService> logger, IMapper mapper, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
        _mapper = mapper;
    }

    public async Task<ProductDto> Get(Guid id, CancellationToken cancellationToken)
    {
        var productEntity = await _databaseContext.Products.FindAsync([id], cancellationToken);
        if (productEntity != null) return _mapper.Map<ProductDto>(productEntity);

        _logger.LogInformation($"Product with id {id} not found");
        throw new Exception("Product not found");
    }

    public async Task<IEnumerable<ProductDto?>> Get(ProductFilterDto data, CancellationToken cancellationToken)
    {
        var query = _databaseContext.Products.AsQueryable();

        if (!string.IsNullOrEmpty(data.Name)) query = query.Where(p => p.Name.Contains(data.Name));

        var productEntities = await query.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ProductDto>>(productEntities);
    }

    public async Task<ProductDto> Create(CreateProductDto data, CancellationToken cancellationToken)
    {
        var category =
            await _databaseContext.Categories.FirstOrDefaultAsync(x => x.Id == data.CategoryId, cancellationToken);

        if (category is null)
        {
            _logger.LogInformation($"Category with id {data.CategoryId} not found");
            throw new Exception("Category not found");
        }

        var productEntity = new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = data.Name,
            Category = category,
            Inventory = new InventoryEntity
            {
                Quantity = 0,
                ProductId = default,
                Id = default
            }
        };
        _databaseContext.Products.Add(productEntity);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductDto>(productEntity);
    }

    public async Task<ProductDto> Update(Guid id, UpdateProductDto data, CancellationToken cancellationToken)
    {
        var productEntity = await _databaseContext.Products.FindAsync([id], cancellationToken);
        if (productEntity == null)
        {
            _logger.LogInformation($"Product with id {id} not found");
            throw new Exception("Product not found");
        }

        var category =
            await _databaseContext.Categories.FirstOrDefaultAsync(x => x.Id == data.CategoryId, cancellationToken);

        if (category is null)
        {
            _logger.LogInformation($"Category with id {data.CategoryId} not found");
            throw new Exception("Category not found");
        }

        productEntity.Name = data.Name;
        productEntity.Category = category;
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductDto>(productEntity);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var productEntity = await _databaseContext.Products.FindAsync([id], cancellationToken);
        if (productEntity == null)
        {
            _logger.LogInformation($"Product with id {id} not found");
            throw new Exception("Product not found");
        }

        _databaseContext.Products.Remove(productEntity);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}