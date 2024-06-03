using Domain.Models;

namespace Business.Interfaces;

public interface IInventoryService
{
    public Task<InventoryDto> AddIncoming(Guid id, UpdateInventoryDto data, CancellationToken cancellationToken);
    public Task<InventoryDto> AddOutgoing(Guid id, UpdateInventoryDto data, CancellationToken cancellationToken);
    public Task<InventoryDto> AddAdjustment(Guid id, UpdateInventoryDto data, CancellationToken cancellationToken);
}