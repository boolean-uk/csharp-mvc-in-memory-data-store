using exercise.wwwapi.Controllers.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Controllers.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
