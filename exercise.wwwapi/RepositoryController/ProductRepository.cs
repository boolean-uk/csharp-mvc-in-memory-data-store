using wwwapi.Data;
using wwwapi.Repository;
using wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace wwwapi.Repository
{


    public class ProductRepository : IRepository<Product, ProductUpdatePayload>
    {
        private ProductContext _db;

        public ProductRepository(ProductContext db)
        {
            _db = db;
        }

        public async Task<List<Product>>   GetAll()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> Add(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;

        }

        public async Task<bool> Delete(int id)
        {
            Product? p = await Get(id);
            if (p == null) { return false; }
            _db.Products.Remove(p);
            await _db.SaveChangesAsync();
            return true;

        }

        public async Task<Product?> Get(int id) {
            Product? product = _db.Products.FirstOrDefault(p => p.id == id);

            if (product == null) { return null; }

            return product;


        }


        public async Task<Product> Update(int id, ProductUpdatePayload payload)
        {
            Product? product = await Get(id);

            if (payload.Name != null) { product.Name = payload.Name; } 
            if (payload.Category != null) { product.Category = payload.Category; } 
            if (payload.Name != null) { product.Name = payload.Name; }

            await _db.SaveChangesAsync();
            return product;
        }
    }
}