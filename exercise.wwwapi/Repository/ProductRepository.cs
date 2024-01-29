using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetProducts(string category)
        {
            if(string.IsNullOrEmpty(category))
            {
                return await _context.Products.ToListAsync();
            }
            return await _context.Products.Where(p => p.Category.Equals(category , StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name.Equals(name , StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Product> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateProduct(int id , Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if(existingProduct == null)
            {
                return;
            }
            existingProduct.Name = product.Name;
            existingProduct.Category = product.Category;
            existingProduct.Price = product.Price;
            existingProduct.DiscountId = product.DiscountId;
            await _context.SaveChangesAsync();
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null)
            {
                return null;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
