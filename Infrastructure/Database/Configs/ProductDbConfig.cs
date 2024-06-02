using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs;

public class ProductDbConfig: IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }

    public static void Seed(EntityTypeBuilder<ProductEntity> builder)
    {
    }
}