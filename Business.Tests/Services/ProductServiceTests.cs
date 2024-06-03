using Business.Interfaces;
using Business.Tests.Helpers;
using Business.Tests.Models;
using Domain.Models;

namespace Business.Tests.Services;

[TestFixture]
public class ProductServiceTests
{
    [SetUp]
    public async Task Setup()
    {
        _serviceFixture = new ServiceFixture(new ServiceFixtureConfiguration());
        _seeder = new Seeder(_serviceFixture.DatabaseContext);

        _productService =
            (_serviceFixture.ServiceProvider.GetService(typeof(IProductService)) as IProductService)!;

        await _serviceFixture.DatabaseContext.Database.EnsureDeletedAsync();
        await _serviceFixture.DatabaseContext.Database.EnsureCreatedAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        await _serviceFixture.DatabaseContext.DisposeAsync();
    }

    private IProductService _productService;
    private ServiceFixture _serviceFixture;
    private Seeder _seeder;

    [Test]
    public async Task Get_ProductExists_ReturnsProductDto()
    {
        // Arrange
        var product = await _seeder.CreateProduct();

        // Act
        var result = await _productService.Get(product.Id, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(product.Id));
            Assert.That(result.Name, Is.EqualTo(product.Name));
        });
    }

    [Test]
    public Task Get_ProductDoesNotExist_ReturnsNull()
    {
        // Arrange
        var nonExistentProductId = Guid.NewGuid();

        // Act && Assert
        Assert.ThrowsAsync<Exception>(
            async () => { await _productService.Get(nonExistentProductId, CancellationToken.None); },
            "Product not found");

        return Task.CompletedTask;
    }

    [Test]
    public async Task Get_ProductsExist_ReturnsListOfProductDto()
    {
        // Arrange
        var category = await _seeder.CreateCategory(new CreateCategoryDto { Name = "Test Category" });
        var product1 =
            await _seeder.CreateProduct(new CreateProductDto { Name = "Product 1", CategoryId = category.Id });
        var product2 =
            await _seeder.CreateProduct(new CreateProductDto { Name = "Product 2", CategoryId = category.Id });

        var filter = new ProductFilterDto { Name = "Product" };

        // Act
        var result = await _productService.Get(filter, CancellationToken.None);

        // Assert
        var productDtos = result.ToList();
        Assert.That(productDtos, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(productDtos, Has.Count.EqualTo(2));
            Assert.That(productDtos.Any(p => p?.Id == product1.Id && p.Name == product1.Name));
            Assert.That(productDtos.Any(p => p?.Id == product2.Id && p.Name == product2.Name));
        });
    }

    [Test]
    public async Task Create_ValidData_ReturnsCreatedProductDto()
    {
        // Arrange
        var category = await _seeder.CreateCategory(new CreateCategoryDto { Name = "Test Category" });
        var createProductDto = new CreateProductDto { Name = "New Product", CategoryId = category.Id };

        // Act
        var result = await _productService.Create(createProductDto, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(result.Name, Is.EqualTo(createProductDto.Name));
            Assert.That(result.Inventory.Product, Is.Null);
            Assert.That(result.Inventory.Quantity, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Update_ProductExists_ReturnsUpdatedProductDto()
    {
        // Arrange
        var product = await _seeder.CreateProduct();
        var updatedProductDto = new UpdateProductDto { Name = "Updated Product", CategoryId = product.Category.Id };

        // Act
        var result = await _productService.Update(product.Id, updatedProductDto, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(product.Id));
            Assert.That(result.Name, Is.EqualTo(updatedProductDto.Name));
        });
    }

    [Test]
    public async Task Delete_ProductExists_ReturnsTrue()
    {
        // Arrange
        var product = await _seeder.CreateProduct();

        // Act
        var result = await _productService.Delete(product.Id, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public Task Delete_ProductDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var nonExistentProductId = Guid.NewGuid();

        // Act && Assert
        Assert.ThrowsAsync<Exception>(
            async () => { await _productService.Get(nonExistentProductId, CancellationToken.None); },
            "Product not found");

        return Task.CompletedTask;
    }

    [Test]
    public async Task Get_ProductFilterIsNull_ReturnsListOfProductDto()
    {
        // Arrange
        var filter = new ProductFilterDto();

        // Act
        var result = await _productService.Get(filter, CancellationToken.None);

        // Assert
        var productDtos = result.ToList();
        Assert.That(productDtos, Is.Not.Null);
        Assert.That(productDtos, Has.Count.EqualTo(0));
    }

    [Test]
    public Task Create_InvalidData_ThrowsException()
    {
        // Arrange
        var createProductDto = new CreateProductDto { Name = "Test Product", CategoryId = 1000 };

        // Act && Assert
        Assert.ThrowsAsync<Exception>(
            async () => { await _productService.Create(createProductDto, CancellationToken.None); },
            "Category not found");

        return Task.CompletedTask;
    }

    [Test]
    [TestCase(true, true, "Category not found")]
    [TestCase(false, false, "Product not found")]
    public async Task Update_InvalidData_ThrowsException(bool useCreatedProduct, bool useNonExistingCategory,
        string exceptionMessage)
    {
        // Arrange
        var product = await _seeder.CreateProduct();
        var createProductDto = new UpdateProductDto
            { Name = "Test Product", CategoryId = useNonExistingCategory ? 1000 : 1 };

        // Act && Assert
        Assert.ThrowsAsync<Exception>(
            async () =>
            {
                await _productService.Update(useCreatedProduct ? product.Id : Guid.NewGuid(), createProductDto,
                    CancellationToken.None);
            },
            exceptionMessage);
    }
}