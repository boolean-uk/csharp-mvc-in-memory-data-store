using exercise.wwwapi.Models;
using exercise.wwwapi.Data;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRepository
    {

        private ProductContext _db;

        public ProductRepository(ProductContext Products)
        {
            _db = Products;
        }

        public Product Add(Product product)
        {
            if (product.Price.GetType() != typeof(int))
            {
                return null;
            }
            _db.Add(product);
            _db.SaveChanges();
            return product;
        }

        public Product deleteAProduct(int Id)
        {
             Product product = getAProduct(Id);
            if (product == null) return null;
            _db.ProductList.Remove(product);
            _db.SaveChanges();
            return product;
        }

        public List<Product> getAll()
        {
            return _db.ProductList.ToList();
        }

        public List<Product> getAll(string category)
        {
            List<Product> filteredList = _db.ProductList.ToList().Where(product => product.Category == category).ToList();
            return filteredList.ToList();
        }

        public Product? getAProduct(int Id)
        {
            return _db.ProductList.FirstOrDefault(product => product.Id == Id);
        }

        public Product updateProduct(int Id, Product updateProduct, out string errorReason)
        {
            if (updateProduct.Price.GetType() != typeof(int))
            {
                errorReason = "type";
                return null;
            }
            Product product = getAProduct(Id);
            if (product == null){
                errorReason = "notfound";
                return null;
            }
            product.Name = updateProduct.Name;
            product.Category = updateProduct.Category;
            product.Price = updateProduct.Price;
            _db.SaveChanges();
            errorReason = "none";
            return product;
        }
    }
}