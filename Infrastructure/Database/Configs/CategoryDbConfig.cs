using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs;

public class CategoryDbConfig : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(50);

        Seed(builder);
    }

    private static void Seed(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasData(new List<CategoryEntity>
        {
            new()
            {
                Id = 1,
                Name = "Eletrônicos"
            },
            new()
            {
                Id = 2,
                Name = "Livros"
            },
            new()
            {
                Id = 3,
                Name = "IoT"
            }
        });
    }
}