using AutoMapper;
using Business.Interfaces;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public class CategoryService : ICategoryService
{
    private readonly DatabaseContext _databaseContext;
    private readonly ILogger<CategoryService> _logger;
    private readonly IMapper _mapper;

    public CategoryService(ILogger<CategoryService> logger, IMapper mapper, DatabaseContext databaseContext)
    {
        _logger = logger;
        _mapper = mapper;
        _databaseContext = databaseContext;
    }

    public async Task<CategoryDto> Get(int id, CancellationToken cancellationToken)
    {
        var categoryEntity = await _databaseContext.Categories.FindAsync([id], cancellationToken);
        if (categoryEntity == null)
        {
            _logger.LogInformation($"Category with id {id} not found");
            throw new Exception("Category not found");
        }

        var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);
        return categoryDto;
    }

    public async Task<IEnumerable<CategoryDto?>> Get(CategoryFilterDto data, CancellationToken cancellationToken)
    {
        var query = _databaseContext.Categories.AsQueryable();

        if (!string.IsNullOrEmpty(data?.Name)) query = query.Where(c => c.Name.Contains(data.Name));

        var categoryEntities = await query.ToListAsync(cancellationToken);
        var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categoryEntities);
        return categoryDtos;
    }

    public async Task<CategoryDto> Create(CreateCategoryDto data, CancellationToken cancellationToken)
    {
        var categoryEntity = new CategoryEntity
        {
            Id = default,
            Name = data.Name
        };

        await _databaseContext.Categories.AddAsync(categoryEntity, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);
        return categoryDto;
    }

    public async Task<CategoryDto> Update(int id, UpdateCategoryDto data, CancellationToken cancellationToken)
    {
        var categoryEntity = await _databaseContext.Categories.FindAsync([id], cancellationToken);
        if (categoryEntity == null)
        {
            _logger.LogInformation($"Category with id {id} not found");
            throw new Exception("Category not found");
        }

        categoryEntity.Name = data.Name;

        await _databaseContext.SaveChangesAsync(cancellationToken);

        var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);
        return categoryDto;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var categoryEntity = await _databaseContext.Categories.FindAsync([id], cancellationToken);
        if (categoryEntity == null)
        {
            _logger.LogInformation($"Category with id {id} not found");
            throw new Exception("Category not found");
        }

        _databaseContext.Categories.Remove(categoryEntity);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}