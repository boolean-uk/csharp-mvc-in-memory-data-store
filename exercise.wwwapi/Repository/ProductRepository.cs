using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository {
    public class ProductRepository : IProductRepository
    {
         private ProductContext _db;

            
        public ProductRepository(ProductContext product) {
            _db = product;
        }

        public Product AddProduct(string Name, string Category, int Price)
        {
            Product product = new Product() {Name = Name, Category = Category, Price = Price};
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public Product? DeleteProduct(int Id)
        {
            Product? product = GetProduct(Id);
            if (product == null)
            {
                return null;
            }
             _db.Products.Remove(product);
            _db.SaveChangesAsync();
            return product;     
        }

        public List<Product> GetAllProducts()
        {
            return [.. _db.Products];
        }

        public Product? GetProduct(int Id)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == Id);
            return product;
        }

        public Product? UpdateProduct(int Id, ProductUpdatePayload updateData)
        {
            Product? product = _db.Products.Find(Id);
            if (product == null)
            {
                return null;
            }

            ComparePayload(ref product, updateData);

            _db.SaveChangesAsync();
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