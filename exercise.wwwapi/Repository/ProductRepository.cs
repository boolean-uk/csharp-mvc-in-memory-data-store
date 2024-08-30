using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.ViewModels;
using System.Runtime.InteropServices;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository
    {
        private DataContext _db;

        public ProductRepository(DataContext db) { _db = db; }

        public Product CreateProduct(Product product)
        {
            Product checkProduct = _db.Products.FirstOrDefault(x => x.Name == product.Name);

            if (checkProduct != null)
            {
                return null;
            }
            
            _db.Add(product);
            _db.SaveChanges();
            return product;
        }

        public List<Product> GetAllProducts(string? category)
        {

            List<Product> products = _db.Products.Where(x => x.Category == category).ToList();
            
            if (category == null) 
            {
                return _db.Products.ToList(); 
            }

            if (products.Count > 0)
            {
                return products;
            }

            return null;
            
        }

        public Product GetAProduct(int id) 
        { 
            
            return _db.Products.Find(id);
        }

        public Product UpdateProduct(int id, Product product)
        {
            Product updateProduct = _db.Products.Find(id);

            if (updateProduct == null)
            {
                return updateProduct;
            }

            updateProduct.Name = product.Name;
            updateProduct.Category = product.Category;
            updateProduct.Price = product.Price;
            _db.SaveChanges();

            return updateProduct;
            
        }

        public Product DeleteProduct(int id) 
        {
            Product deleteProduct = GetAProduct(id);

            if ( deleteProduct != null)
            {
                _db.Remove(deleteProduct);
                _db.SaveChanges();
                
            }

            return deleteProduct;

        }
    }
}
