using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class MyStoreContext : DbContext
    {
        public MyStoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products {get; set;}
        public DbSet<Brand> Brands {get; set;}
        public DbSet<Category> Categories {get; set;}
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}