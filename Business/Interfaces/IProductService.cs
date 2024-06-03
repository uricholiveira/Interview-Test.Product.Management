using Domain.Models;

namespace Business.Interfaces;

public interface IProductService
{
    public Task<ProductDto> Get(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<ProductDto?>> Get(ProductFilterDto data, CancellationToken cancellationToken);
    public Task<ProductDto> Create(CreateProductDto data, CancellationToken cancellationToken);
    public Task<ProductDto> Update(Guid id, UpdateProductDto data, CancellationToken cancellationToken);
    public Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}