using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository
    {
        private ProductContext _db;

        public ProductRepository(ProductContext db)
        {
            _db = db;
        }

        public List<Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }
        public Product? AddProduct(string Name, string Category, string Price)
        {
            int i;
            if (!int.TryParse(Price, out i) || _db.Products.Any (p => p.name == Name))
            {
                return null;
            }
            var product = new Product() { name = Name, category = Category, price = i };

            _db.Add(product);
            _db.SaveChanges();
            return product; 
        }
        public Product? GetProduct(int id)
        {
            return _db.Products.FirstOrDefault(p => p.id == id);
        }

        public Product? UpdateProduct(int id, ProductUpdatePayload updateData)
        {
            var Product = GetProduct(id);
            if(Product == null)
            {
                return null;
            }
            bool hasUpdate = false;

            if(updateData.Name != null)
            {
                Product.name = (string)updateData.Name;
                hasUpdate = true;
            }
            if(updateData.Category != null)
            {
                Product.category = (string)updateData.Category;
                hasUpdate = true;
            }
            if (updateData.Price != null)
            {
                Product.price = (int)updateData.Price;
                hasUpdate = true;
            }

            if (!hasUpdate) throw new Exception("No update data");
            
            _db.SaveChanges();

            return Product;

        }
        public Product? RemoveProduct(int id)
        {
            var product = GetProduct(id);
            if (product == null) throw new Exception("No delete data");
            _db.Products.Remove(product);
            _db.SaveChanges();
            return product;
        }




    }
}
