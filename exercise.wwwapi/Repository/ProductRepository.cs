using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext _db;
        public ProductRepository(ProductContext db)
        {
            _db = db;
        }
        public async Task<Product> Create(ProductPayload data)
        {
            if (await _db.products.AnyAsync(x => x.name == data.name))
            {
                throw new Exception();
            }
            Product newProduct = new Product() { name = data.name, category = data.category, price = data.price};
            _db.Add(newProduct);
            _db.SaveChanges();
            return newProduct;
        }

        public async Task<Product> Delete(int id)
        {
            Product? product = await _db.products.FirstOrDefaultAsync(x => x.id == id);
            if (product is null) throw new Exception();
            _db.products.Remove(product);
            _db.SaveChanges();
            return product;
        }

        public async Task<List<Product>> GetAll()
        {
           return await _db.products.ToListAsync();
        }

        public async Task<Product?> GetProductByID(int id)
        {
        //    return _db.products.FirstOrDefault(x => x.id == id);
            return await _db.products.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<Product?> Update(int id, ProductPayload data)
        {
            if (await _db.products.AnyAsync(x => x.name == data.name && x.id != id))
            {
                throw new Exception();
            }
            Product? result = await GetProductByID(id);
            if (result is null) return null;
            result.name = data.name;
            result.price = data.price;
            result.category = data.category;
            _db.SaveChanges();
            return result;
        }
    }
}
