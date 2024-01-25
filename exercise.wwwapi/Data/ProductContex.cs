using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Data
{
    public class ProductContex : DbContext
    {
        public ProductContex(DbContextOptions<ProductContex> options) : base(options) { }

        public DbSet<InternalProduct> Products { get; set; }
    }
}
