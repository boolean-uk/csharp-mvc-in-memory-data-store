using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private ProductContext _db;
        private int _id = 0;

        public ProductRepository(ProductContext db) {
            _db = db;
        }

        public Product AddProduct(ProductPostPayload payload)
        {
            var newProduct = new Product() { Id = getNextId(), Name = payload.name, Category = payload.category, Price = payload.price };
            _db.Add(newProduct);
            _db.SaveChanges();
            return newProduct;
        }

        private int getNextId()
        {
            return _id++;
        }

        public bool DeleteProduct(int id)
        {
            var deletedProduct = getProductById(id);
            if (deletedProduct == null)
            {
                return false;
            }
            _db._products.Remove(deletedProduct);
            _db.SaveChanges();
            return true;
        }

        public List<Product> getAllProducts()
        {
            return _db._products.ToList();
        }

        public Product? getProductById(int id)
        {
            return _db._products.FirstOrDefault(p => p.Id == id);
        }

        public Product UpdateProduct(int _id, ProductPutPayload payload)
        {
            var updatedProduct = getProductById(_id);
            if (updatedProduct == null)
            {
                return null;
            }

            bool isUpdated = false;

            if (payload.name != null && payload.name.Length > 0) {
                updatedProduct.Name = payload.name;
                isUpdated = true;
            }

            if (payload.category != null && payload.category.Length > 0)
            {
                updatedProduct.Category = payload.category;
                isUpdated = true;
            }

            if (payload.price != null && payload.price.Value > 0)
            {
                updatedProduct.Price = payload.price.Value;
                isUpdated = true;
            }

            if (!isUpdated)
            {
                throw new Exception("No update payload entered"); 
            }

            _db.SaveChanges();

            return updatedProduct;
        }
    }
}
