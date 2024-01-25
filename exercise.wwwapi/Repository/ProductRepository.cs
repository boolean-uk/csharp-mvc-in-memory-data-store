using exercise.wwwapi.Models;
using exercise.wwwapi.Data;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository: IProductRepository
    {
        // Get Database context
       private ProductContext _db;

         public ProductRepository(ProductContext db)
         {
              _db = db;
         }

        // GetAllProducts method returns a list of all products
        public List<Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        public Product? GetProduct(int id)
        {
            return _db.Products.FirstOrDefault(p => p.Id == id);
        }

        // AddProduct method takes a name, category and price as parameters
        public Product AddProduct(string name, string category, int price)
        {
            var product = new Product() { Name = name, Category = category, Price = price };
            _db.Add(product);
            _db.SaveChanges();
            return product;
        }

        // UpdateProduct method takes an id and a ProductUpdatePayload object as parameters
        public Product? UpdateProduct(int id, ProductUpdatePayload productUpdatePayload)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            product.Name = productUpdatePayload.Name;
            product.Category = productUpdatePayload.Category;
            product.Price = productUpdatePayload.Price;
            _db.SaveChanges();
            return product;
        }

        // DeleteProduct method takes an id as parameter
        public Product? DeleteProduct(int id)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            return product;
        }

    }
}