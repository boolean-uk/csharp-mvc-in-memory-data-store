using Microsoft.EntityFrameworkCore;
using wwwapi.Models;

namespace wwwapi.Data
{
    public class ProductContext : DbContext
    {


        public DbSet<Product> Products { get; set; }
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product("How to build APIs", "Book", 1500)
            ); ;


        }
    }
}