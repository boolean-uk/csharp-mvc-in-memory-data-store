using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace workshop.wwwapi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ProductCollectionHelper products = new ProductCollectionHelper();

            modelBuilder.Entity<Product>().HasData(new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 1200 });

        }

        public DbSet<Product> Products { get; set; }
    }
}
