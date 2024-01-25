using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using System.Diagnostics.Metrics;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext _db;
        public ProductRepository(ProductContext db)
        {
            _db = db;
        }
        public Product Create(ProductPayload data)
        {
            Product newProduct = new Product() { name = data.name, category = data.category, price = data.price};
            _db.Add(newProduct);
            _db.SaveChanges();
            return newProduct;
        }

        public Product Delete(int id)
        {
            Product? product = _db.products.FirstOrDefault(x => x.id == id);
            if (product is null) throw new Exception();
            _db.products.Remove(product);
            _db.SaveChanges();
            return product;
        }

        public List<Product> GetAll()
        {
           return _db.products.ToList();
        }

        public Product? GetProductByID(int id)
        {
            return _db.products.FirstOrDefault(x => x.id == id);
        }

        public Product? Update(int id, ProductPayload data)
        {
            Product? result = GetProductByID(id);
            if (result is null) return null;
            result.name = data.name;
            result.price = data.price;
            result.category = data.category;
            _db.SaveChanges();
            return result;
        }
    }
}
