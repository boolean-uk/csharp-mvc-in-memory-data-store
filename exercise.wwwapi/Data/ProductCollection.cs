
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using static System.Reflection.Metadata.BlobBuilder;

namespace exercise.wwwapi.Data
{
    public class ProductCollection : DbContext
    {
        public ProductCollection(DbContextOptions<ProductCollection> options) : base(options) { }

        public DbSet<Product> products { get; set; }

       
    }
}
