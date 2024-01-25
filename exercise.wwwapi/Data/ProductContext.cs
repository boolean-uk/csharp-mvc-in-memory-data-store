using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Data
{
    public class ProductContext : DbContext
    {
        // Constructor for ProductContext
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            
        }
        // DbSet for Product
        public DbSet<Product> Products { get; set; }
    }
}