using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Extensions;

public static class MigrationManager
{
    public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext, IDisposable
    {
        using (var scope = host.Services.CreateScope())
        {
            var requiredService = scope.ServiceProvider.GetRequiredService<T>();
            try
            {
                requiredService.Database.Migrate();
            }
            finally
            {
                if (requiredService != null)
                    ((IDisposable)requiredService).Dispose();
            }
        }

        return host;
    }
}