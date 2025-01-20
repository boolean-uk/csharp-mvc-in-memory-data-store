using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using exercise.wwwapi.View;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private static int _uniqueIDCounter = 0;
        private static int GetUniqueID()
        {
            return ++_uniqueIDCounter;
        }
        private DataContext _context;
        public ProductRepository(DataContext context) 
        {
            this._context = context;
        }
        public async Task<Product?> CreateProduct(Product_create dto)
        {
            Product product = new Product { 
                id = GetUniqueID(),
                category = dto.category,
                name = dto.name,
                price = dto.price
            };
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();

            // returns result of Find will guarantee that we only return if insertion was valid
            return await _context.FindAsync<Product>(product.id);
            
        }

        public async Task<Product?> DeleteProduct(int id)
        {
            var product = await GetProduct(id);
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> GetProduct(int id)
        {
            return await _context.products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts(string category)
        {
            if (category != "")
                return await _context.products.Where(x => x.category == category).ToListAsync();
            else
                return await _context.products.ToListAsync();
        }

        public async Task<Product?> UpdateProduct(int id, Product_edit dto)
        {
            var product = await GetProduct(id);
            //Product_create createDto = new Product_create();
            product.price     = dto.price    ?? product.price;
            product.category  = dto.category ?? product.category;
            product.name      = dto.name     ?? product.name;
            //return await CreateProduct(createDto);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> GetProduct(string name)
        {
            return await _context.products.Where(x => x.name == name).FirstOrDefaultAsync();
        }
    }
}
