using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Model;


namespace exercise.wwwapi.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options) 
            {
            }
            
        public DbSet<Product> Products { get; set; }
    }
}
