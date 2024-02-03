using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        { 

        }
        public DbSet<Product> Product { get; set; }
    }
}
