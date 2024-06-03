using Application.Models;
using AutoMapper;
using Business.Interfaces;
using Business.Services;
using Business.Tests.Helpers.Database;
using Business.Tests.Models;
using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Business.Tests.Helpers;

public class ServiceFixture
{
    public ServiceFixture(ServiceFixtureConfiguration configuration, ServiceCollection? serviceCollection = null)
    {
        var services = serviceCollection ?? [];
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        RepositoryFixture = new RepositoryFixture();
        DatabaseContext = InMemoryDb.GetNewInMemoryDbContext();

        var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        Mapper = mapperConfig.CreateMapper();

        services
            .AddSingleton(loggerFactory)
            .AddSingleton(provider => provider.GetService<ILoggerFactory>()!.CreateLogger<ProductService>())
            .AddSingleton(provider => provider.GetService<ILoggerFactory>()!.CreateLogger<CategoryService>())
            .AddSingleton(provider => provider.GetService<ILoggerFactory>()!.CreateLogger<InventoryService>());

        services
            .AddSingleton<IProductService>(provider =>
                new ProductService(
                    provider.GetService<ILogger<ProductService>>()!,
                    Mapper,
                    DatabaseContext
                ));

        services
            .AddSingleton<ICategoryService>(provider =>
                new CategoryService(
                    provider.GetService<ILogger<CategoryService>>()!,
                    Mapper,
                    DatabaseContext
                ));

        services
            .AddSingleton<IInventoryService>(provider =>
                new InventoryService(
                    provider.GetService<ILogger<InventoryService>>()!,
                    Mapper,
                    DatabaseContext
                ));

        ServiceProvider = services.BuildServiceProvider();
    }

    public ServiceProvider ServiceProvider { get; set; }
    public DatabaseContext DatabaseContext { get; set; }
    public IMapper Mapper { get; set; }
    public RepositoryFixture RepositoryFixture { get; set; }
}