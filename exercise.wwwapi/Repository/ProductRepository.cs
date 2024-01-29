using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext _db;
        public ProductRepository(ProductContext db)
        {
            _db = db;
        }
        public async Task<List<Product>> GetAll()
        {
            var product = await _db.Products.ToListAsync();
            return product;
        }
        public Product? GetProduct(int id)
        {
            return _db.Products.FirstOrDefault(p => p.Id == id);
        }
        public Product AddProduct(string name, string category, int price)
        {
            var product = new Product() { name = name, category = category, price = price};
            _db.Add(product);
            _db.SaveChanges();
            return product;
        }
        public Discount AddDiscount(int discountAmount ,int productId)
        {
            var discount = new Discount() { PriceOff = discountAmount, ProductId = productId};
            _db.Add(discount);
            _db.SaveChanges();
            return discount;
        }
        public Product? UpdateProduct(int id, ProductUpdatePayload updateData)
        {
            var product = GetProduct(id);
            if(product == null)
            {
                return null;
            }
            bool hasUpdate = false;

            if (updateData.name != null)
            {
                product.name = updateData.name;
                hasUpdate = true;
            }
            if (updateData.category != null)
            {
                product.category = updateData.category;
                hasUpdate = true;
            }
            if (updateData.price != null)
            {
                product.price = (int)updateData.price;
                hasUpdate = true;
            }

            if (!hasUpdate) throw new Exception("No update data provided");
            _db.SaveChanges();
            return product;
        }
        public Product? RemoveProduct(int id)
        {
            var product = GetProduct(id);
            if (product == null)
            {
                return null;
            }
            _db.Remove(product);
            _db.SaveChanges();
            return product;
        }
    }
}
