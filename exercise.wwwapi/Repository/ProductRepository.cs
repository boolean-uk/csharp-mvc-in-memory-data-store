using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository: IProductRepository
    {
        private DataContext _db;
        public ProductRepository(DataContext db)
        {
            _db = db;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _db.Products.ToListAsync();
            return products;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<Product> AddAsync(ProductCreatePayload payload)
        {
            var product = new Product() { Name = payload.Name, Category = payload.Category, Price = payload.Price};
            await _db.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateAsync(int id, ProductUpdatePayload productData)
        {
            var product = await GetByIdAsync(id);
            if (product == null)
            {
                return null;
            }

            if (productData.Name != null)
            {
                product.Name = (string)productData.Name;
            }

            if (productData.Category != null)
            {
                product.Category = (string)productData.Category;
            }

            if (productData.Price != null)
            {
                product.Price = (double)productData.Price;
            }

            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product == null) return false;
            _db.Remove(product);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
