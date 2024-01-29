using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        public ProductContext _db;

        public ProductRepository(ProductContext db)
        {
            _db = db;
        }

        public List<Product> GetAllProducts(string? filter)
        {

            if (filter != null)
            {
                return _db.Products.Where(x => x.Category == filter).ToList();
            }
            return _db.Products.ToList();
        }

        public Product AddProduct(string name, string category, int price)
        {
        
            var prod = new Product { Name = name, Category = category, Price = price };

            _db.Products.Add(prod);
            _db.SaveChanges();

            return prod;
        }

        public Product? GetProduct(int id)
        {
            return _db.Products.FirstOrDefault(x => x.Id == id);
        }

        public Product? UpdateProduct(int id, ProductUpdatePayload updateData)
        {
            var prod = GetProduct(id);

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

            _db.SaveChanges();

            return prod;
        }

        public bool DeleteProduct(int id)
        {
            var prod = GetProduct(id);

            if (prod == null) return false;

            _db.Products.Remove(prod); 
            _db.SaveChanges();
            return true;
        }
    }
}
