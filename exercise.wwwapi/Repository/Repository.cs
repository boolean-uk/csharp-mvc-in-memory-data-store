using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using System.Diagnostics.Eventing.Reader;

namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private DataContext _db;
        public Repository(DataContext db)
        {
            _db = db;
        }
        public Product UpdateProduct(int id, ProductPut productPut)
        {
            var found = _db.Products.FirstOrDefault(x => x.Id == id);
            if (found == null)
            {
                return null;
            }
            found.Name = productPut.Name;
            found.Category = productPut.Category;
            found.Price = productPut.Price;
            _db.SaveChanges();
            return found;
        }

        public Product AddProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;

        }

        public IEnumerable<Product> GetProducts()
        {
            return _db.Products.ToList();
        }
        public IEnumerable<Product> GetProducts(string category)
        {
            return _db.Products.Where(x => x.Category == category).ToList();
        }

        public Product GetAProduct(int id)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == id);
            return product;
        }
        public Product DeleteProduct(int id)
        {
            var found = _db.Products.FirstOrDefault(x => x.Id == id);
            if (found == null)
            {
                return null;
            }
            _db.Products.Remove(found);
            _db.SaveChanges();
            return found;
        }
    }
}
