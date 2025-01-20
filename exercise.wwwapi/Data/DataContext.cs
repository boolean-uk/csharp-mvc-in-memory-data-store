using genericapi.api.Models;
using Microsoft.EntityFrameworkCore;

namespace genericapi.api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
