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
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<Product> AddProduct(Product model)
        {
            await _db.Products.AddAsync(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<Product> UpdateProduct(int id, ProductPut model)
        {
            Product product = await _db.Products.FindAsync(id);
            if (product == null) return null;
            if (model.name != null) product.name = model.name;
            if (model.category != null) product.category = model.category;
            if (model.price != null) product.price = (int)model.price;
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var target = await _db.Products.FindAsync(id);
            _db.Remove(target);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
