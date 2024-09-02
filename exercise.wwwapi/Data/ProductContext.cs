using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Data
{
    public class ProductContext(DbContextOptions<ProductContext> options) : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
    }
}
