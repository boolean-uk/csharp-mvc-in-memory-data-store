using exercise.wwwapi.Models.Discounts;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options) { }

        public DbSet<Discount> _discounts { get; set; }

    }
}
