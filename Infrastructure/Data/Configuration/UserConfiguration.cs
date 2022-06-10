﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class UsuarioConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.Property(p => p.Id)
                .IsRequired();
        builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);
        builder.Property(p => p.Lastname)
                .IsRequired()
                .HasMaxLength(200);
        builder.Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(200);
        builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(200);

        builder
        .HasMany(p => p.Roles)
        .WithMany(p => p.Users)
        .UsingEntity<UsersRoles>(
            j => j
                .HasOne(pt => pt.Rol)
                .WithMany(t => t.UsersRoles)
                .HasForeignKey(pt => pt.RolId),
            j => j
                .HasOne(pt => pt.User)
                .WithMany(p => p.UsersRoles)
                .HasForeignKey(pt => pt.UserId),
            j =>
            {
                j.HasKey(t => new { t.UserId, t.RolId });
            });

        builder.HasMany(p => p.RefreshTokens)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

    }
}
