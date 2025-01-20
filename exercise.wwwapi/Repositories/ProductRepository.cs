using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private DataContext _db;

        public ProductRepository(DataContext db)
        {
            _db = db;
        }
        public async Task<Product> AddProduct(Product entity)
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
        public async Task<Product> UpdateProduct(Product entity)
        {
            var target = await _db.Products.FindAsync(entity.Id);
            _db.Entry(target).CurrentValues.SetValues(entity);
            await _db.SaveChangesAsync();
            return target;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            return await _db.Products.Where(p => p.category == category).ToListAsync(); 
        }
    }
}
