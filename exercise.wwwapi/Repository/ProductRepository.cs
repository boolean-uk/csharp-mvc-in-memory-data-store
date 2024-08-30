using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private ProductContext _db;
        public ProductRepository(ProductContext db)
        {
            _db = db;
        }

        public Product AddProduct(Product product)
        {
            if(_db.Products.Any(p => p.Name == product.Name))
            {
                return null;
            }
            _db.Add(product);
            _db.SaveChanges();
            return product;
        }

        public Product DeleteProduct(int id)
        {
            Product p = _db.Products.Find(id);
            if (p != null)
            {
                _db.Remove(p);
                _db.SaveChanges();
            }
            return p;
        }
        
        public List<Product> GetAllProducts(string category)
        {
            if(category == null)
            {
                return _db.Products.ToList();
            }
            return _db.Products.Where(p => p.Category == category).ToList();
        }

        public Product GetProduct(int id)
        {
            return _db.Products.Find(id);
        }

        public Product UpdateProduct(int id, Product product)
        {
            Product p = _db.Products.Find(id);

            p.Name = product.Name;
            p.Category = product.Category;
            p.Price = product.Price;
            _db.SaveChanges();
            return p;
        }
    }
}
