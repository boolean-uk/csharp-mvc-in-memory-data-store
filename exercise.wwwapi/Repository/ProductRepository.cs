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
        public Product CreateProduct(Product product)
        {
            _db.products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public Product DeleteProduct(int id)
        {
            Product product = _db.products.Find(id);
            if (product != null) 
            { 
                _db.Remove(product);
                _db.SaveChanges();
            }
            return product;
        }

        /* public List<Product> GetAll()
         {
             return _db.products.ToList();
         }*/

        public List<Product> GetAll(string? category)
        {
            if (category != null)
            {
                return _db.products.ToList().Where(x => x.Category.Equals(category)).ToList();
            }
            return _db.products.ToList();
        }
        public Product GetProductById(int id)
        {
            Product product = _db.products.Find(id);
            return product;
        }

        public Product UpdateProduct(int id, Product product)
        {
            Product updateproduct = _db.products.Find(id);
            if (updateproduct != null)
            {
                updateproduct.Name = product.Name;
                updateproduct.Price = product.Price;
                updateproduct.Category = product.Category;
                _db.SaveChanges();
            }
            return updateproduct;
        }
    }
}
