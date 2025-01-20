using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.UUID);
        }

        public static void SeedData(DataContext context)
        {
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product("Bagel", "Food", 1),
                    new Product("Coffee", "Drink", 2),
                    new Product("Muffin", "Food", 3)
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
