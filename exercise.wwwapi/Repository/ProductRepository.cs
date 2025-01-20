using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Data;
using workshop.wwwapi.Models;
using workshop.wwwapi.ViewModel;

namespace workshop.wwwapi.Repository
{
    public class ProductRepository  : IProductRepository
    {
        private DataContext _db;

        public ProductRepository(DataContext db)
        {
            _db = db;
        }
        public async Task<Product> GetProduct(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts(string category)
        {
            if (!string.IsNullOrEmpty(category))
            {
                var categoryExists = await _db.Products.AnyAsync(p => p.Category.ToLower() == category);

                if (categoryExists)
                {
                    return await _db.Products.Where(p => p.Category.ToLower() == category).ToListAsync();
                }

                return null;
            }
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> AddProduct(Product entity)
        {       
            await _db.Products.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
            
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var target = await _db.Products.FindAsync(id);
            _db.Products.Remove(target);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Product> UpdateProduct (Product model)
        {
            _db.Products.Update(model);
            await _db.SaveChangesAsync();
            return model;
        }
        
    }
}
