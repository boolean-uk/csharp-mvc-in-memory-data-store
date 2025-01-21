using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Model;
using System.Collections.Generic;


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
