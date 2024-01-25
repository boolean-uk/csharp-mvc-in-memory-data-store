using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _db;

        public ProductRepository(ProductDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<Product> GetProducts() 
        {
            return _db.Products.ToList();   
        }

        public Product GetProductById (int id) 
        {
            return _db.Products.Find(id);
        }

        public Product CreateProduct (Product product) 
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public Product UpdateProduct (int id, ProductPut productPut) 
        {
            var existingProduct = _db.Products.Find(id);
            if (existingProduct == null) 
            {
                return null;
            }

            existingProduct.Price = productPut.Price;
            existingProduct.Category = productPut.Category;
            existingProduct.Name = productPut.Name;

            _db.SaveChanges();
            return existingProduct;
        }

        public void DeleteProduct (int id)
        {
            var product = _db.Products.Find(id);

            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }
    }
}
