using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using exercise.wwwapi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository
    {
        private DataContext _db;
        public ProductRepository(DataContext db)
        {
            _db = db;
        }


        public Product AddProduct(Product entity)
        {
            _db.Products.Add(entity);
            _db.SaveChanges();
            return entity;
            
        }

        public Product DeleteProduct(int id)
        {
            Product entity = _db.Products.FirstOrDefault(x => x.Id == id);
            _db.Remove(entity);
            return entity;
         }

        public Product GetProduct(int id)
        {
            return _db.Products.Find(id);
        }

        public List<Product> GetProducts(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return _db.Products.ToList();
            }

            return _db.Products.Where(x => x.Category == category).ToList();
        }

    

        public Product UpdateProduct(int id, Product product)
        {
            Product prod = _db.Products.Find(id);

            _db.Entry(prod).CurrentValues.SetValues(product);

            _db.SaveChanges();

            return prod;
        }
    }
}
