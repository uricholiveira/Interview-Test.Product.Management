using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs;

public class InventoryDbConfig : IEntityTypeConfiguration<InventoryEntity>
{
    public void Configure(EntityTypeBuilder<InventoryEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Product)
            .WithOne(y => y.Inventory)
            .HasForeignKey<InventoryEntity>(b => b.ProductId);

        builder
            .HasMany(x => x.Transactions)
            .WithOne(y => y.Inventory)
            .HasForeignKey(y => y.InventoryId);
    }

    public static void Seed(EntityTypeBuilder<InventoryEntity> builder)
    {
    }
}