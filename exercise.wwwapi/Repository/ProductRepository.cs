using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private DataContext _db;
        public ProductRepository(DataContext db)
        {
            _db = db;
        }

        public IEnumerable<Product> GetProducts(string? category)
        {
            if(string.IsNullOrEmpty(category))
            {
                return _db.Products.ToList();
            }

            return _db.Products.Where(x => x.Category == category);
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        public Product AddProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public Product GetAProduct(int id)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == id);
            return product;
        }

        public Product DeleteProduct(int id)
        {
            Product prod = _db.Products.FirstOrDefault(x => x.Id == id);
            _db.Products.Remove(prod);
            _db.SaveChanges();
            return prod;
        }

        public Product UpdateProduct(int id, ProductPut productPut)
        {
            Product prod = _db.Products.FirstOrDefault(p => p.Id == id);
            prod.Price = productPut.Price;
            prod.Name = productPut.Name;
            prod.Category = productPut.Category;
            _db.SaveChanges();
            return prod;
        }
    }
}
