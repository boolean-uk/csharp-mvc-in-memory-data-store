using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Data
{
    public interface IDataContext
    {
        public DbSet<Product> Products { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options), IDataContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
