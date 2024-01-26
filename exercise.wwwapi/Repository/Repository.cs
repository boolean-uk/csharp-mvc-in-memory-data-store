using exercise.wwwapi.Models;
using exercise.wwwapi.Data;

namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private DataContext _db;
        public Repository(DataContext db)
        {
            _db = db;
        }

        public Product Add(ProductParameters product)
        {
            Product newProduct = new Product(product.Name, product.Category, product.Price);
            _db.Add(newProduct);
            _db.SaveChanges();
            return newProduct;
        }

        public Product Delete(Product product)
        {
            _db.Remove(product);
            _db.SaveChanges();
            return product;
        }

        public Product Get(int id)
        {
            Product product = _db.Products.FirstOrDefault(p => p.Id == id);
            _db.SaveChanges();
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products;
        }

        public IEnumerable<Product> GetAll(string category)
        {
            return _db.Products.Where(p => p.Category == category);
        }

        public Product Update(Product product, ProductParameters newData)
        {
            Product newProduct = product.Update(new Product(newData.Name, newData.Category, newData.Price));
            _db.SaveChanges();
            return newProduct;
        }
    }
}
