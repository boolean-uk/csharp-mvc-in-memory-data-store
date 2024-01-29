using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository {
    public class ProductRepository : IProductRepository
    {
         private readonly ProductContext _db;

            
        public ProductRepository(ProductContext product) {
            _db = product;
        }

        public async Task<Product> AddProduct(string Name, string Category, int Price)
        {
            Product product = new Product() {Name = Name, Category = Category, Price = Price};
            await _db.Products.AddAsync(product);
            _db.SaveChanges();
            return product;
        }

        public async Task<Product> DeleteProduct(int Id)
        {
            Product? product = await GetProduct(Id);
            if (product == null)
            {
                return null;
            }
             _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return product;     
        }

        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> list = await _db.Products.ToListAsync();
            return list;
        }

        public async Task<Product> GetProduct(int Id)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<Product> UpdateProduct(int Id, ProductUpdatePayload updateData)
        {
            Product? product = await _db.Products.FindAsync(Id);
            if (product == null)
            {
                return null;
            }

            ComparePayload(ref product, updateData);

            await _db.SaveChangesAsync();
            return product;
        }


        private Product ComparePayload(ref Product product, ProductUpdatePayload data)
        {
            bool hasUpdate = false;
            if (data.Name != null)
            {
                product.Name = data.Name;
                hasUpdate = true;
            }

            if (data.Category != null)
            {
                product.Category = data.Category;
                hasUpdate = true;
            }

            if (data.Price != null)
            {
                product.Price = (int)data.Price;
                hasUpdate = true;
            }
            if(!hasUpdate) throw new Exception("No update data provided");
            return product;
        }
    }
}