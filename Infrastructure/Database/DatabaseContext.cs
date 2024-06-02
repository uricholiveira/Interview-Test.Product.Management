
using Infrastructure.Abstracts;
using Infrastructure.Database.Configs;
using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<InventoryEntity> Inventories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductDbConfig());
        modelBuilder.ApplyConfiguration(new CategoryDbConfig());
        modelBuilder.ApplyConfiguration(new InventoryDbConfig());

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetUpdateAndCreatedDateTimeOnChangedDbEntities<Guid>();
        SetUpdateAndCreatedDateTimeOnChangedDbEntities<int>();

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        SetUpdateAndCreatedDateTimeOnChangedDbEntities<Guid>();
        SetUpdateAndCreatedDateTimeOnChangedDbEntities<int>();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void SetUpdateAndCreatedDateTimeOnChangedDbEntities<T>()
    {
        var entries = ChangeTracker.Entries<DbEntity<T>>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntries in entries)
            switch (entityEntries.State)
            {
                case EntityState.Added:
                    entityEntries.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entityEntries.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
    }
}