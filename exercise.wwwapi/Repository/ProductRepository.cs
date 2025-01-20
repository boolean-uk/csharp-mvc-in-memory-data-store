using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
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
            await _db.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            Product p = await _db.Products.FirstOrDefaultAsync(p => p.id == id);
            if (p == null)
            {
                return null;
            }
            _db.Products.Remove(p);
            await _db.SaveChangesAsync();
            return p;
        }

        public async Task<Product> UpdateProduct(Product product, int id)
        {
            var p_edit = await _db.Products.FirstAsync(p => p.id == id);

            p_edit.name = product.name;
            p_edit.price = product.price;
            p_edit.category = product.category;
            await _db.SaveChangesAsync();

            return p_edit;
        }

        public Task<Product?> GetProduct(int id)
        {
            return _db.Products.FirstAsync(p => p.id == id);
        }
        
        public async Task<Product> GetProductByName(string name)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.name == name);
        }


        public async Task<IEnumerable<Product>> GetProducts(string category)
        {
            return _db.Products.Where(p => p.category == category).ToList();
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }

    }
}
