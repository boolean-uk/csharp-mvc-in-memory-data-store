using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private DataContext _db;
        public ProductRepository(DataContext db)
        {
            _db = db;
        }

        public Product Add(Product model)
        {
            _db.Add(model);
            _db.SaveChanges();
            return model;
        }

        public List<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public List<Product> GetAll(string category)
        {
            return _db.Products.Where(p => p.Category == category).ToList();
        }

        public Product Get(int id)
        {
            var product = _db.Products.Find(id);
            return product;
        }

        public Product Update(int id, Product model)
        {
            Product product = Get(id);
            product.Name = model.Name;
            product.Category = model.Category;
            product.Price = model.Price;
            _db.SaveChanges();
            return product;
        }

        public Product Delete(int id)
        {
            Product product = Get(id);
            if (product == null) return null;
            _db.Remove(product);
            _db.SaveChanges();
            return product;
        }
    }
}
