using Domain.Models;

namespace Business.Interfaces;

public interface ICategoryService
{
    public Task<CategoryDto> Get(int id, CancellationToken cancellationToken);
    public Task<IEnumerable<CategoryDto?>> Get(CategoryFilterDto data, CancellationToken cancellationToken);
    public Task<CategoryDto> Create(CreateCategoryDto data, CancellationToken cancellationToken);
    public Task<CategoryDto> Update(int id, UpdateCategoryDto data, CancellationToken cancellationToken);
    public Task<bool> Delete(int id, CancellationToken cancellationToken);
}