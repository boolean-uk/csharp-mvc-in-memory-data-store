using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

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
