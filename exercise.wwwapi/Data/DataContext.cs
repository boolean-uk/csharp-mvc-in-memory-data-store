using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Data
{
    public class DataContext<T> : DbContext where T : Products //For now..
    {
        public DataContext(DbContextOptions<DataContext<T>> options) : base(options)
        {
            
        }
        public DbSet<T> products { get; set; }   

    }
}
