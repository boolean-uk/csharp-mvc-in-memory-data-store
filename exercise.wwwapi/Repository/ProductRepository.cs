using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    //Create repositpry for all products
    public class ProductRepository:IproductRepository
    {
        //private ProductCollection _products;

        private ProductContext _db;

        public ProductRepository(ProductContext db)
        {
            _db = db;
        }

        public Product CreateProduct(string name, string category, int price)
        {
            var product = new Product() { Name = name, Category = category, Price = price };
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public List<Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        public Product? GetProductById(int id)
        {
            return _db.Products.FirstOrDefault(x => x.Id == id);
        }

        public Product UpdateProduct(int id, string newName, string newCategory, int newPrice)
        {
            var product = GetProductById(id);
            if (product == null)
            {
                return null;
            }
            else
            {
                product.Name = newName;
                product.Category = newCategory;
                product.Price = newPrice;
                _db.SaveChanges();
                return product;
            }

        }

        public Product DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product == null)
            {
                return null;
            }
            else
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
                return product;
            }
        }
    }
}
