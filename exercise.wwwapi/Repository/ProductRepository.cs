using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository
    {
        private DataContext _db;

        public ProductRepository(DataContext db)
        {
            _db = db;
        }
        public async Task<Product> Add(Product entity)
        {
            await _db.Products.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;

        }

        public async Task<bool> Delete(int id)
        {
            var target = await _db.Products.FindAsync(id);
            _db.Products.Remove(target);
            await _db.SaveChangesAsync();
            return true;


        }

        public async Task<Product> Get(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAll(string category = "None")
        {
            if (category == "None")
            {
                return await _db.Products.ToListAsync();
            }
            else
            {
                return await _db.Products.Where(x => x.Category == category).ToListAsync();
            }
        }
    }
}
