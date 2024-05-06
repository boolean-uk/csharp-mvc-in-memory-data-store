using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using System.ComponentModel;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext _db;

        public ProductRepository(ProductContext db)
        {
            _db = db;
        }

        public List<Products> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        public Products AddProduct(string productName, string category, int price)
        {
            var product = new Products { Name = productName, Category = category, Price = price};
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public bool ProductExists(string productName)
        {
            return _db.Products.Any(p => p.Name == productName);
        }

        public Products? GetProductById(int id)
        {
            return _db.Products.FirstOrDefault(p => p.Id == id);
        }

        public Products? UpdateProduct(int id, ProductUpdatePayload updateProductData)
        {
            var products = GetProductById(id);
            if (products == null) return null;
            bool hasUpdate = false;

            if(updateProductData.Name != null && updateProductData.Category != null) 
            {
                products.Name = (string)updateProductData.Name;
                products.Category = (string)updateProductData.Category;
                products.Price = (int)updateProductData.Price;
                hasUpdate = true;
            }

            if (!hasUpdate) throw new Exception("Nu updated data provided.");

            _db.SaveChanges();
            return products;
        }

        public bool DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product == null) return false;
            
           
                _db.Products.Remove(product);
                _db.SaveChanges();
            return true;
        }

    }
}
