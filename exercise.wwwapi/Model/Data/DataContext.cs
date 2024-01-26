using exercise.wwwapi.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Model.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
