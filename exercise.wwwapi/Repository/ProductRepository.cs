using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepo
    {
        DataContext _db;

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

        public void DeleteProduct(int id)
        {
            Product product = _db.products.FirstOrDefault(x => x.Id == id);
            _db.Remove(product);
            _db.SaveChanges();
     
        }

        public List<Product> GetProducts(string category)
        {
           return _db.products.Where(x => x.Category == category).ToList();
        }

        public Product GetProduct(int id)
        {
            return _db.products.FirstOrDefault(x => x.Id == id);
        }

        public Product UpdateProduct(Product product, int id)
        {
            Product foundproduct = GetProduct(id);
            foundproduct.Name = product.Name;   
            foundproduct.Price = product.Price;
            foundproduct.Category = product.Category;
            _db.SaveChanges();
            return foundproduct;
        }

        public List<Product> GetAll()
        {
            return _db.products.ToList();
        }
    }
}
