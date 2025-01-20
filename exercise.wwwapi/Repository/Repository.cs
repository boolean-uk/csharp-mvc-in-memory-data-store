using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private DataContext _db;

        public Repository(DataContext db)
        {
            _db = db;
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            _db.Products.Remove(await _db.Products.FindAsync(id));
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _db.Products.ToListAsync();        
        }

        public async Task<bool> UpdateProduct(int id)
        {
            var target = await _db.Products.FindAsync(id);
            _db.Products.Update(target);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
