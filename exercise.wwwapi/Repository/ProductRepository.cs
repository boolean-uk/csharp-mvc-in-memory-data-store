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
        public Product CreateProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public IEnumerable<Product> GetProducts(string category = null)
        {
            if(category == null)
            {
                return _db.Products.ToList();
            }
            else
            {
                return _db.Products.Where(p => p.Category == category).ToList();
            }
        }

        public Product GetAProduct(int id)
        {
            var car = _db.Products.FirstOrDefault(x => x.Id == id);
            return car;
        }

        public Product UpdateProduct(int id, ProductPut product)
        {
            var foundProduct = GetAProduct(id);
            if(foundProduct == null)
            {
                return null;
            }
            foundProduct.Name = product.Name;
            foundProduct.Category = product.Category;
            foundProduct.Price = product.Price;
            return foundProduct;
        }

        public Product DeleteProduct(int id)
        {
            Product removeThis = GetAProduct(id);
            _db.Products.Remove(removeThis);
            return  removeThis;
        }

    }
}
