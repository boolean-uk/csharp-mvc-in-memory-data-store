using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using System.Xml.Linq;

namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private DataContext _db;
        public Repository(DataContext db) 
        {
            _db = db;
        }


        public List<Product> GetProducts(string? category)
        {
            if (category == null)
            {
                return _db.Products.ToList();
            }
            return _db.Products.ToList().Where(p => p.Category.ToLower() == category.ToLower()).ToList();
            
        }

        public Product GetProduct(int id)
        {
            return _db.Products.FirstOrDefault(p => p.Id == id);
        }
        public Product UpdateProduct(int id, ProductBody product)
        {
            Product p = _db.Products.FirstOrDefault(x => x.Id == id);
            if (p == null)
            {
                return null;
            }
            p.Name = product.Name;
            p.Category = product.Category;
            p.Price = product.Price;
            _db.SaveChanges();
            return p;
        }

        public void DeleteProduct(int id)
        {
            _db.Products.Remove(_db.Products.FirstOrDefault(p => p.Id == id));
            _db.SaveChanges();
        }

        public Product AddProduct(ProductBody product)
        {
            Product p = new();
            if(_db.Products.Count() > 0)
            {
                p.Id = _db.Products.Last().Id + 1;
            }
            else
            {
                p.Id = 1;
            }
            
            p.Name = product.Name;
            p.Category = product.Category;
            p.Price = product.Price;
            _db.Products.Add(p);
            _db.SaveChanges();
            return p;
        }

        public bool NameExists(string name)
        {
            if (_db.Products.ToList().Where(p => p.Name.ToLower() == name.ToLower()).Count() > 0)
            {
                return true;
            }
            return false;
        }

        public bool IdExists(int id)
        {
            if (_db.Products.ToList().Where(p => p.Id== id).Count() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
