using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private DataContext _db;

        public ProductRepository(DataContext db) 
        {
            _db = db;
        }
        public Product CreateProduct(Product product)
        {
            _db.Add(product);
            _db.SaveChanges();
            return product;
        }

        public List<Product> GetAllProducts(string category)
        {
            var filtered = _db.Products.Where(x => x.Category == category).ToList();
            return filtered;
        }

        public Product GetProductByName(string name)
        {
            var filtered = _db.Products.FirstOrDefault(x => x.Name == name);
            return filtered;
        }

        public Product GetProduct(int id) 
        {
            var match = _db.Products.FirstOrDefault(x => x.Id == id);
            return match;
        }

        public Product UpdateProduct(Product product)
        {
            _db.Attach(product);
            _db.Entry(product).State = EntityState.Modified;
            _db.SaveChanges();
            return product;
        }

        public Product DeleteProduct(int id)
        {
            var match = _db.Products.FirstOrDefault(x => x.Id == id);
            _db.Remove(match);
            _db.SaveChanges();
            return match;
        }
    }
}
