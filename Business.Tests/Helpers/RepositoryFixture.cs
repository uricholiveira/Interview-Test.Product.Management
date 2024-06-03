using Business.Tests.Helpers.Database;
using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Tests.Helpers;

public class RepositoryFixture
{
    public RepositoryFixture()
    {
        var services = new ServiceCollection();
        DatabaseContext = InMemoryDb.GetNewInMemoryDbContext();

        // services.AddSingleton<IRepository<T>>(_ => new Repository<T>(DatabaseContext));

        ServiceProvider = services.BuildServiceProvider();
    }

    public ServiceProvider ServiceProvider { get; }
    public DatabaseContext DatabaseContext { get; set; }
}