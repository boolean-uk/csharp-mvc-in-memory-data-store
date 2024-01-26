using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Data
{
    public abstract class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
    }
}