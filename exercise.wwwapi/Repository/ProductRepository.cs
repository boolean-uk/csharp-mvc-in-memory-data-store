using exercise.wwwapi.Data;
using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository: IProductRepository
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

        public Product? GetById(int id)
        {
            return _db.Products.FirstOrDefault(p => p.Id == id);
        }

        public Product Add(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public Product? Update(int id, ProductUpdatePayload productData)
        {
            var product = GetById(id);
            if (product == null)
            {
                return null;
            }

            if (productData.Name != null)
            {
                product.Name = (string)productData.Name;
            }

            if (productData.Category != null)
            {
                product.Category = (string)productData.Category;
            }

            if (productData.Price != null)
            {
                product.Price = (double)productData.Price;
            }

            _db.SaveChanges();
            return product;
        }

        public bool Delete(int id)
        {
            var product = GetById(id);
            if (product == null) return false;
            _db.Products.Remove(product);
            _db.SaveChanges();
            return true;
        }
    }
}
