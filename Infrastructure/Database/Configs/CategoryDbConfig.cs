using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs;

public class CategoryDbConfig: IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }

    public static void Seed(EntityTypeBuilder<CategoryEntity> builder)
    {
    }
}