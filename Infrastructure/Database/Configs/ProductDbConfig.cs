using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs;

public class ProductDbConfig : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(50);

        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId);

        builder
            .HasOne(x => x.Inventory)
            .WithOne(y => y.Product)
            .HasForeignKey<InventoryEntity>(b => b.ProductId);
    }

    public static void Seed(EntityTypeBuilder<ProductEntity> builder)
    {
    }
}