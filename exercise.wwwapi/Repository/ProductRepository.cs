using exercise.wwwapi.Data;
using exercise.wwwapi.DTO;
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
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Product> Delete(int id)
        {
            var product = await Get(id);

            if (product == null) return null;

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Get(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAll(string? category = null)
        {
            return category != null ? await _db.Products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToListAsync() : await _db.Products.ToListAsync();
        }

        public async Task<Product> Update(Product entity)
        {
            _db.Products.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
