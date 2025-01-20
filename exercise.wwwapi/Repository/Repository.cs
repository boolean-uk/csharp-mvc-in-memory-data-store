using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Data;
using workshop.wwwapi.Models;
using workshop.wwwapi.ViewModel;

namespace workshop.wwwapi.Repository
{
    public class Repository  : IRepository
    {
        private DataContext _db;

        public Repository(DataContext db)
        {
            _db = db;
        }
        public async Task<Product> AddProduct(Product entity)
        {       
            await _db.Products.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
            
        }

        public async Task<Product> PutProduct(Product entity)
        {
            _db.Products.Update(entity);
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

        public async Task<IEnumerable<Product>> GetProduct(string? category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return await _db.Products.ToListAsync();
            }

            var products = await _db.Products.Where(x => x.Category == category).ToListAsync();
            return products; 
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> GetProductName(string name)
        {
            return await _db.Products.FindAsync(name);
        }
        public async Task<Product> GetProductId(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<Product> UpdateProduct(Product entity)
        {
             _db.Products.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

    }
}
