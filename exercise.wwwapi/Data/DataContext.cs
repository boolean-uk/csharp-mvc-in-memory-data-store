using exercise.wwwapi.model;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    public required DbSet<Product> Products { get; set; }
}
