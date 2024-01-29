using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        public ProductContext _db;

        public ProductRepository(ProductContext db)
        {
            _db = db;
        }

        public async Task<List<Product>> GetAllProducts(string? filter)
        {

            if (filter != null)
            {
                return await _db.Products.Where(x => x.Category == filter).ToListAsync();
            }
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> AddProduct(string name, string category, int price)
        {

            // Discount disc = _db.Discounts.First(x => x.Id == discId);

            var prod = new Product { Name = name, Category = category, Price = price};

            
            _db.Add(prod);
            await _db.SaveChangesAsync();

            return prod;
        }

        public async Task<Product?> GetProduct(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> UpdateProduct(int id, ProductUpdatePayload updateData)
        {
            var prod = await GetProduct(id);

            if (prod == null)
            {
                return null;
            }

            bool upName = false;
            bool upCategory = false;
            bool upPrice = false;

            if (updateData.name != null && updateData.name.Length > 0)
            {
                prod.Name = (string)updateData.name;
                upName = true;
            }

            if (updateData.category != null && updateData.category.Length > 0)
            {
                prod.Category = (string)updateData.category;
                upCategory = true;
            }

            if (updateData.price != null && updateData.price >= 0)
            {
                prod.Price = (int)updateData.price;
                upPrice = true;
            }

            if (!upName || !upPrice || !upCategory) 
            {
                throw new Exception("No product update!");
            }

            await _db.SaveChangesAsync();

            return prod;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var prod = await GetProduct(id);

            if (prod == null) return false;

            _db.Products.Remove(prod); //Products.Remove(prod); 
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
