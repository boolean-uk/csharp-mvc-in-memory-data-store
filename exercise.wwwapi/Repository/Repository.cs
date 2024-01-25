using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private DataContext _db;

        public Repository(DataContext db)
        {
            _db = db;
        }

        public Product? AddProduct(Product product)
        {
            if (_db.Products.FirstOrDefault(p => p.Name == product.Name) != null)
            {
                return null;
            }

            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public IEnumerable<Product> GetProducts(string category)
        {
            return _db.Products.Where(product => product.Category == category).ToList();
        }

        public Product? GetAProduct(int id)
        {
            return _db.Products.FirstOrDefault(product => product.Id == id);
        }

        public Product? UpdateProduct(int id, ProductPost product)
        {

            var newProduct = _db.Products.FirstOrDefault(p => p.Id == id);

            if (newProduct == null)
            {
                return null;
            }

            if (_db.Products.FirstOrDefault(p => p.Name == product.Name) != null)
            {
                newProduct.Name = null;
                return newProduct;
            }

            newProduct.Name = product.Name;
            newProduct.Category = product.Category.ToLower();
            newProduct.Price = int.Parse(product.Price);
            _db.SaveChanges();
            return newProduct;
        }

        public Product? DeleteProduct(int id)
        {
            var deletedProduct = _db.Products.FirstOrDefault(product => product.Id == id);

            if (deletedProduct == null)
            {
                return null;
            }

            _db.Remove(deletedProduct);
            _db.SaveChanges();
            return deletedProduct;
        }
    }
}
