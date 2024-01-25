using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        public DbSet<Product> Product { get; set; }
    }
}
