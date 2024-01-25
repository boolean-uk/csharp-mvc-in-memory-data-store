using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Model;

namespace exercise.wwwapi.Data
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options)
            : base(options)
        {

        }

        public DbSet<Products> Products { get; set; }

    }
}


  