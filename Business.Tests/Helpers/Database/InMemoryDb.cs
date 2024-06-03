using System.Runtime.CompilerServices;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Business.Tests.Helpers.Database;

public class InMemoryDb : DatabaseContext
{
    public InMemoryDb(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public static InMemoryDb GetNewInMemoryDbContext([CallerMemberName] string? databaseName = null)
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName ?? Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        return new InMemoryDb(options);
    }
}