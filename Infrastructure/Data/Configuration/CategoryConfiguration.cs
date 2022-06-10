using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categogry");

        builder.Property(p => p.Id)
                .IsRequired();

        builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
    }
}
