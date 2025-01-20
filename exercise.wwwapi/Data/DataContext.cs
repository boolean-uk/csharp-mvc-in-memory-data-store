using genericapi.api.Models;
using Microsoft.EntityFrameworkCore;

namespace genericapi.api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product() { Name = "Fanta", Category = "Soft drink", Price = 12 },
                new Product() { Name = "Cola", Category = "Soft drink", Price = 13 },
                new Product() { Name = "Bread", Category = "Baked goods", Price = 3 }
            );
        }
        public DbSet<Product> Products { get; set; }
    }
}
