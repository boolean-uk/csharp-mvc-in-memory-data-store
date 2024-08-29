using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Data
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

    }
}
