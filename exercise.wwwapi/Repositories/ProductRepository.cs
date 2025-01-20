using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repositories
{
    public class ProductRepository : IRepository
    {
        private DataContext _db;
        public ProductRepository(DataContext db)
        {
            _db = db;
        }
        public async Task<Product> AddProduct(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            Product target = await _db.Products.FindAsync(id);
            if (target != null)
            {
                _db.Products.Remove(target);
                await _db.SaveChangesAsync();
                return target;
            }
            return null;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts(string category)
        {
            return await _db.Products.Where(p => p.Category == category).ToListAsync();
        }

        public async Task<Product> UpdateProduct(Product product, int id)
        {
            Product target = await _db.Products.FindAsync(id);
            if (target != null)
            {
                target.Price = product.Price;
                target.Name = product.Name;
                target.Category = product.Category;
                _db.Update(target);
                await _db.SaveChangesAsync();
                return target;
            }
            return null;

        }
    }
}
