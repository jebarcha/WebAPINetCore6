using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasIndex(e => e.CategoryId, "IX_Product_CategoryId");
        
        builder.HasIndex(e => e.BrandId, "IX_Product_BrandId");
        
        builder.Property(e => e.CreationDate).HasMaxLength(6);
        
        builder.Property(p => p.Id).IsRequired();

        builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2");

        builder.HasOne(p => p.Brand)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.BrandId);

        builder.HasOne(p => p.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.CategoryId);
    }
}
