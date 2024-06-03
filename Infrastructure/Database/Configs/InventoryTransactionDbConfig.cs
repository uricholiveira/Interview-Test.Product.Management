using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs;

public class InventoryTransactionDbConfig : IEntityTypeConfiguration<InventoryTransactionEntity>
{
    public void Configure(EntityTypeBuilder<InventoryTransactionEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }

    public static void Seed(EntityTypeBuilder<InventoryTransactionEntity> builder)
    {
    }
}