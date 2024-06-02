using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs;

public class InventoryDbConfig: IEntityTypeConfiguration<InventoryEntity>
{
    public void Configure(EntityTypeBuilder<InventoryEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }

    public static void Seed(EntityTypeBuilder<InventoryEntity> builder)
    {
    }
}