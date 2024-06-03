using Business.Interfaces;
using Business.Tests.Helpers;
using Business.Tests.Models;
using Domain.Enums;
using Domain.Models;

namespace Business.Tests.Services;

[TestFixture]
public class InventoryServiceTests
{
    [SetUp]
    public async Task Setup()
    {
        _serviceFixture = new ServiceFixture(new ServiceFixtureConfiguration());
        _seeder = new Seeder(_serviceFixture.DatabaseContext);

        _inventoryService =
            (_serviceFixture.ServiceProvider.GetService(typeof(IInventoryService)) as IInventoryService)!;

        await _serviceFixture.DatabaseContext.Database.EnsureDeletedAsync();
        await _serviceFixture.DatabaseContext.Database.EnsureCreatedAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        await _serviceFixture.DatabaseContext.DisposeAsync();
    }

    private IInventoryService _inventoryService;
    private ServiceFixture _serviceFixture;
    private Seeder _seeder;

    [Test]
    public async Task AddIncoming_Should_AddIncomingInventory()
    {
        // Arrange
        const int quantity = 10;
        var data = new UpdateInventoryDto
        {
            Quantity = quantity, MovementMovementType = EInventoryTransactionMovementType.Incoming
        };

        var product = await _seeder.CreateProduct();

        // Act
        var result = await _inventoryService.AddIncoming(product.Inventory.Id, data, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(product.Inventory.Id, result.Id);
        Assert.AreEqual(quantity, result.Quantity);
    }

    [Test]
    public async Task AddOutgoing_Should_AddOutgoingInventory()
    {
        // Arrange
        const int quantity = 5;
        var data = new UpdateInventoryDto
        {
            Quantity = quantity, MovementMovementType = EInventoryTransactionMovementType.Outgoing
        };

        var product = await _seeder.CreateProduct();

        // Act
        var result = await _inventoryService.AddOutgoing(product.Inventory.Id, data, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(product.Inventory.Id, result.Id);
        Assert.AreEqual(quantity * -1, result.Quantity);
    }

    [Test]
    public async Task AddAdjustment_Should_AddAdjustmentInventory()
    {
        // Arrange
        const int quantity = 3;
        var data = new UpdateInventoryDto
        {
            Quantity = quantity, MovementMovementType = EInventoryTransactionMovementType.Adjustment
        };

        var product = await _seeder.CreateProduct();

        // Act
        var result = await _inventoryService.AddAdjustment(product.Inventory.Id, data, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(product.Inventory.Id, result.Id);
        Assert.AreEqual(quantity, result.Quantity);
    }

    [Test]
    public Task AddIncoming_Should_ThrowException_When_ProductNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid();
        const int quantity = 10;
        var data = new UpdateInventoryDto
        {
            Quantity = quantity, MovementMovementType = EInventoryTransactionMovementType.Incoming
        };

        // Act and Assert
        Assert.ThrowsAsync<Exception>(
            async () => { await _inventoryService.AddIncoming(productId, data, CancellationToken.None); },
            "Inventory not found");
        return Task.CompletedTask;
    }

    [Test]
    public Task AddOutgoing_Should_ThrowException_When_ProductNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid();
        const int quantity = 5;
        var data = new UpdateInventoryDto
        {
            Quantity = quantity, MovementMovementType = EInventoryTransactionMovementType.Outgoing
        };

        // Act and Assert
        Assert.ThrowsAsync<Exception>(
            async () => { await _inventoryService.AddOutgoing(productId, data, CancellationToken.None); },
            "Inventory not found");
        return Task.CompletedTask;
    }

    [Test]
    public Task AddAdjustment_Should_ThrowException_When_ProductNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid();
        const int quantity = 3;
        var data = new UpdateInventoryDto
        {
            Quantity = quantity, MovementMovementType = EInventoryTransactionMovementType.Adjustment
        };

        // Act and Assert
        Assert.ThrowsAsync<Exception>(
            async () => { await _inventoryService.AddAdjustment(productId, data, CancellationToken.None); },
            "Inventory not found");
        return Task.CompletedTask;
    }
}