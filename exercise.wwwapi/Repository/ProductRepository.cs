using exercise.wwwapi.Context;
using exercise.wwwapi.Model;
using System.Xml.Linq;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext _db;

        public ProductRepository(ProductContext db)
        {
            _db = db;
        }

        public Product AddProduct(Product product)
        {
            _db.Product.Add(product);
            _db.SaveChanges();
            return product;
        }

        public Product DeleteProduct(int id)
        {
            var product = _db.Product.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            _db.Product.Remove(product);
            _db.SaveChanges();
            return product;
        }

        public Product GetAProduct(int id)
        {
            return _db.Product.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _db.Product.ToList();
        }

        public Product UpdateProduct(int id, ProductPut productPut)
        {
            var found = _db.Product.FirstOrDefault(x => x.Id == id);
            if (found == null)
            {
                return null;
            }
            found.name = productPut.name;
            found.category = productPut.category;
            found.price = (int)productPut.price;
            _db.SaveChanges();
            return found;
        }
    }
}
