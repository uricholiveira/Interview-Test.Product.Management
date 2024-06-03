using Business.Interfaces;
using Business.Tests.Helpers;
using Business.Tests.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Tests.Services;

[TestFixture]
public class CategoryServiceTests
{
    [SetUp]
    public async Task Setup()
    {
        _serviceFixture = new ServiceFixture(new ServiceFixtureConfiguration());
        _seeder = new Seeder(_serviceFixture.DatabaseContext);

        _categoryService =
            (_serviceFixture.ServiceProvider.GetService(typeof(ICategoryService)) as ICategoryService)!;

        await _serviceFixture.DatabaseContext.Database.EnsureDeletedAsync();
        await _serviceFixture.DatabaseContext.Database.EnsureCreatedAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        await _serviceFixture.DatabaseContext.DisposeAsync();
    }

    private ICategoryService _categoryService;
    private ServiceFixture _serviceFixture;
    private Seeder _seeder;

    [Test]
    public async Task Get_CategoryExists_ReturnsCategoryDto()
    {
        // Arrange
        var category = await _seeder.CreateCategory();

        // Act
        var result = await _categoryService.Get(category.Id, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(category.Id));
    }

    [Test]
    public Task Get_NonExistingCategory_ThrowsAsync()
    {
        // Arrange
        var categoryId = Random.Shared.Next(100, 200);

        // Act && Assert
        Assert.ThrowsAsync<Exception>(
            async () => { await _categoryService.Get(categoryId, CancellationToken.None); },
            "Category not found");

        return Task.FromResult(Task.CompletedTask);
    }

    [Test]
    public async Task Get_CategoriesExist_ReturnsListOfCategoryDto()
    {
        // Arrange
        var count = await _serviceFixture.DatabaseContext.Categories.CountAsync();
        await _seeder.CreateCategory(new CreateCategoryDto { Name = "Category 1" });
        await _seeder.CreateCategory(new CreateCategoryDto { Name = "Category 2" });

        var filter = new CategoryFilterDto();

        // Act
        var result = await _categoryService.Get(filter, CancellationToken.None);

        // Assert
        var categoryDtos = result.ToList();
        Assert.That(categoryDtos, Is.Not.Null);
        Assert.That(categoryDtos, Has.Count.EqualTo(count + 2));
    }

    [Test]
    public async Task Create_ValidCategoryDto_ReturnsCreatedCategoryDto()
    {
        // Arrange
        var categoryDto = new CreateCategoryDto { Name = "Test Category" };

        // Act
        var result = await _categoryService.Create(categoryDto, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(categoryDto.Name));
    }

    [Test]
    public async Task Update_CategoryExists_ReturnsUpdatedCategoryDto()
    {
        // Arrange
        var category = await _seeder.CreateCategory();

        var updatedCategoryDto = new UpdateCategoryDto { Name = "Updated Category" };

        // Act
        var result = await _categoryService.Update(category.Id, updatedCategoryDto, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(updatedCategoryDto.Name));
    }

    [Test]
    public Task Update_NonExistingCategory_ThrowsAsync()
    {
        // Arrange
        var categoryId = Random.Shared.Next(100, 200);

        var updatedCategoryDto = new UpdateCategoryDto { Name = "Updated Category" };

        // Act && Assert
        Assert.ThrowsAsync<Exception>(
            async () => { await _categoryService.Update(categoryId, updatedCategoryDto, CancellationToken.None); },
            "Category not found");
        return Task.CompletedTask;
    }

    [Test]
    public async Task Delete_CategoryExists_ReturnsTrue()
    {
        // Arrange
        var category = await _seeder.CreateCategory();
        var categoryId = category.Id;

        // Act
        var result = await _categoryService.Delete(categoryId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public Task Delete_CategoryDoesNotExist_ThrowsAsync()
    {
        // Arrange
        var categoryId = Random.Shared.Next(100, 200);

        // Act && Assert
        Assert.ThrowsAsync<Exception>(
            async () => { await _categoryService.Delete(categoryId, CancellationToken.None); },
            "Category not found");

        return Task.FromResult(Task.CompletedTask);
    }

    [Test]
    public async Task Get_CategoriesExistWithFilter_ReturnsFilteredListOfCategoryDto()
    {
        // Arrange
        await _seeder.CreateCategory(new CreateCategoryDto { Name = "Category 1" });
        await _seeder.CreateCategory(new CreateCategoryDto { Name = "Category 2" });

        var filter = new CategoryFilterDto { Name = "Category" };

        // Act
        var result = await _categoryService.Get(filter, CancellationToken.None);

        // Assert
        var categoryDtos = result.ToList();
        Assert.That(categoryDtos, Is.Not.Null);
        Assert.That(categoryDtos, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task Get_CategoriesExistWithFilter_ReturnsEmptyList()
    {
        // Arrange
        await _seeder.CreateCategory(new CreateCategoryDto { Name = "Category 1" });
        await _seeder.CreateCategory(new CreateCategoryDto { Name = "Category 2" });

        var filter = new CategoryFilterDto { Name = "Nonexistent" };

        // Act
        var result = await _categoryService.Get(filter, CancellationToken.None);

        // Assert
        var categoryDtos = result.ToList();
        Assert.That(categoryDtos, Is.Not.Null);
        Assert.That(categoryDtos, Is.Empty);
    }
}