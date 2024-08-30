using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Data
{
    public class ProductDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }  

        public ProductDataContext(DbContextOptions<ProductDataContext> options) : base(options)
        {

        }

    }
}
