using exercise.wwwapi.Interface;
using exercise.wwwapi.Model;
using exercise.wwwapi.DataContext;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDataContext _db;

        public ProductRepository(ApplicationDataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> GetSingleProduct(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(int id, Product product)
        {
            var existingProduct = await _db.Products.FindAsync(id);

            existingProduct.Name = product.Name;
            existingProduct.Category = product.Category;
            existingProduct.Price = product.Price;

            await _db.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Product?> GetProductByName(string name)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
