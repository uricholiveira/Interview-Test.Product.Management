using AutoMapper;
using Business.Interfaces;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public class InventoryService : IInventoryService
{
    private readonly DatabaseContext _databaseContext;
    private readonly ILogger<InventoryService> _logger;
    private readonly IMapper _mapper;

    public InventoryService(ILogger<InventoryService> logger, IMapper mapper, DatabaseContext databaseContext)
    {
        _logger = logger;
        _mapper = mapper;
        _databaseContext = databaseContext;
    }

    public async Task<InventoryDto> AddIncoming(Guid id, UpdateInventoryDto data, CancellationToken cancellationToken)
    {
        var inventoryEntity = await _databaseContext.Inventories.Include(i => i.Product)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

        if (inventoryEntity == null)
        {
            _logger.LogError($"Inventory with id {id} not found.");
            throw new Exception("Inventory not found");
        }

        var transactionEntity = new InventoryTransactionEntity
        {
            InventoryId = inventoryEntity.Id,
            Quantity = data.Quantity,
            MovementMovementType = data.MovementMovementType,
            Id = default
        };

        await _databaseContext.InventoryTransactions.AddAsync(transactionEntity, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        inventoryEntity.Quantity += data.Quantity;

        await _databaseContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InventoryDto>(inventoryEntity);
    }

    public async Task<InventoryDto> AddOutgoing(Guid id, UpdateInventoryDto data, CancellationToken cancellationToken)
    {
        var inventoryEntity = await _databaseContext.Inventories.Include(i => i.Product)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

        if (inventoryEntity == null)
        {
            _logger.LogError($"Inventory with id {id} not found.");
            throw new Exception("Inventory not found");
        }

        var transactionEntity = new InventoryTransactionEntity
        {
            InventoryId = inventoryEntity.Id,
            Quantity = data.Quantity * -1,
            MovementMovementType = data.MovementMovementType,
            Id = default
        };

        await _databaseContext.InventoryTransactions.AddAsync(transactionEntity, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        inventoryEntity.Quantity -= data.Quantity;

        await _databaseContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InventoryDto>(inventoryEntity);
    }

    public async Task<InventoryDto> AddAdjustment(Guid id, UpdateInventoryDto data,
        CancellationToken cancellationToken)
    {
        var inventoryEntity = await _databaseContext.Inventories.Include(i => i.Product)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

        if (inventoryEntity == null)
        {
            _logger.LogError($"Inventory with id {id} not found.");
            throw new Exception("Inventory not found");
        }

        var transactionEntity = new InventoryTransactionEntity
        {
            InventoryId = inventoryEntity.Id,
            Quantity = inventoryEntity.Quantity > data.Quantity
                ? inventoryEntity.Quantity - data.Quantity
                : inventoryEntity.Quantity + data.Quantity,
            MovementMovementType = data.MovementMovementType,
            Id = default
        };

        await _databaseContext.InventoryTransactions.AddAsync(transactionEntity, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        inventoryEntity.Quantity = data.Quantity;

        await _databaseContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InventoryDto>(inventoryEntity);
    }
}