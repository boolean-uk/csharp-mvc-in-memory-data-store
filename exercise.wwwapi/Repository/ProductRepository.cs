using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository: IProductRepository
    {
        private ProductContext _db;
        public ProductRepository(ProductContext db)
        {
            _db = db;
        }
        public async Task<List<Product>> GetAll()
        {
            var products = await _db.Products.ToListAsync();
            return products;
        }

        public async Task<Product?> GetById(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<Product> Add(ProductCreatePayload payload)
        {
            var product = new Product() { Name = payload.Name, Category = payload.Category, Price = payload.Price};
            await _db.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> Update(int id, ProductUpdatePayload productData)
        {
            var product = await GetById(id);
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

        public async Task<bool> Delete(int id)
        {
            var product = await GetById(id);
            if (product == null) return false;
            _db.Remove(product);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
