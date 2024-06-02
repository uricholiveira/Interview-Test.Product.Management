using Domain.Dtos;

namespace Business.Interfaces;

public interface IProductService
{
    public Task<ProductDto?> Get(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<ProductDto?>> Get(CancellationToken cancellationToken);
    public Task<ProductDto?> Create(ProductDto data, CancellationToken cancellationToken);
    public Task<ProductDto?> Update(Guid id, ProductDto data, CancellationToken cancellationToken);
    public Task<ProductDto?> Delete(Guid id, CancellationToken cancellationToken);
}