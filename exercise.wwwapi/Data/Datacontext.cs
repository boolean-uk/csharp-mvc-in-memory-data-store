using exercise.wwwapi.Model;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;


namespace exercise.wwwapi.Data
{
    public class Datacontext : DbContext
    {
        public Datacontext(DbContextOptions<Datacontext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "How to build APIs", Category = "Book", Price = 1500 }
            );
            */
        }

        public DbSet<Product> Products { get; set; }

    }
}
