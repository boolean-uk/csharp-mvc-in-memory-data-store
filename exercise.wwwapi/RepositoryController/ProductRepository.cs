using wwwapi.Data;
using wwwapi.Repository;
using wwwapi.Models;

namespace wwwapi.Repository
{


    public class ProductRepository : IRepository<Product, ProductPayload>
    {
        private ProductContext _db;

        public ProductRepository(ProductContext db)
        {
            _db = db;
        }

        public List<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public Product Add(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;

        }

        public bool Delete(int id)
        {
            Product? p = Get(id);
            if (p != null) { return false; }
            _db.Products.Remove(p);
            _db.SaveChanges();
            return true;

        }

        public Product? Get(int id) {
            Product? product = _db.Products.FirstOrDefault(p => p.id == id);

            if (product == null) { return null; }

            return product;


        }


        public Product Update(int id, ProductPayload payload)
        {
            Product product = Get(id);

            if (payload.Name != null) { product.Name = payload.Name; } 
            if (payload.Category != null) { product.Category = payload.Category; } 
            if (payload.Name != null) { product.Name = payload.Name; }

            _db.SaveChanges();
            return product;
        }
    }
}