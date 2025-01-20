using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        
        { 
            
            
        }

        public DbSet<Product> Products { get; set; }

    }
}
