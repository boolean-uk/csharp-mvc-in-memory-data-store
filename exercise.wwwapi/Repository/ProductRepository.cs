using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private DataContext _db;

        public ProductRepository(DataContext db)
        {
            _db = db;
        }
        public async Task<Product> AddProduct(Product product)
        {
            //int id = _db.Products.Max(x => x.Id) + 1;
            //product.Id = id;    
            await _db.AddAsync(product); 
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<bool> Contains(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            return true;

        }

        public async Task<Product> DeleteProductById(int id)
        {

            var target = await _db.Products.FindAsync(id);

            if (target != null)
            {
                _db.Products.Remove(target);
                await _db.SaveChangesAsync();
                return target;
            }
            return null;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return _db.Products.Count() == 0 ? null : await _db.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {

            var target = await _db.Products.FindAsync(id);
            return target == null ? null : target;
        }

        public async Task<Product> UpdateProduct(int id, string name, string category, int price)
        {
            var product = await _db.Products.FindAsync(id);

            if (product != null)
            {
                product.Price = price;
                product.Name = name;
                product.Category = category;
                await _db.SaveChangesAsync();
                return product;
            }

            return null;

        }
    }
}
