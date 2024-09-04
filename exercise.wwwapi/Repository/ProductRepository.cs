using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository<Product>
    {

        private ProductDataContext _db;


        public ProductRepository(ProductDataContext db)
        {
            _db = db;

        }

        public Product Create(Product product)
        {
           _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public Product Get(int id)
        {
            return _db.Products.FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetAllProducts(string Category)
        {
            if (string.IsNullOrWhiteSpace(Category))
            {
                return _db.Products.ToList();
            }
            else
            {
                return _db.Products.Where(x => x.Category == Category).ToList();
            }
        }

        public Product Delete(int id)
        {
            Product deletedproduct = _db.Products.FirstOrDefault(x => x.Id == id);

            _db.Products.Remove(deletedproduct);
            _db.SaveChanges();

            return deletedproduct;
        }

        public Product Update(Product entity, int id)
        {
            throw new NotImplementedException();
        }

        public Product GetByName(string Name)
        {
            var product = _db.Products.FirstOrDefault(x => x.Name == Name);
            return product;

        }
    }
}
