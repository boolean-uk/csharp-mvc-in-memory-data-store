using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using exercise.wwwapi.View_Models;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository
    {
        private DataContext _db;
        public ProductRepository(DataContext db)
        {
            _db = db;
        }
        public Product AddProduct(Product product)
        {
            _db.Add(product);
            _db.SaveChanges();
            return product;
        }

        public Product ChangeProduct(ProductPostModel newProduct, int id)
        {
            Product foundProduct = _db.Products.FirstOrDefault(x => x.Id == id);
            foundProduct.Name = newProduct.Name;
            foundProduct.Price = newProduct.Price;
            foundProduct.Category = newProduct.Category;
            _db.SaveChanges();
            return foundProduct;
        }

        public bool ContainsProduct(string name)
        {
            if (_db.Products.Select(x => x.Name).ToList().Contains(name))
            {
                return true;
            }
            return false;
        }
        public bool ContainsProduct(int id)
        {
            if (_db.Products.Select(x => x.Id).ToList().Contains(id))
            {
                return true;
            }
            return false;
        }

        public bool IsEmpty()
        {
            if (_db.Products.Count() == 0)
                return true;

            return false;
        }

        public string DeleteProduct(int id)
        {
            Product searchResult = _db.Products.FirstOrDefault(y => y.Id == id);
            _db.Remove(searchResult);
            _db.SaveChanges(true);
            return "Product Deleted";

        }

        public Product GetProduct(int id)
        {
            Product product = _db.Products.FirstOrDefault(x => x.Id == id);
            return product;
        }

        public List<Product> GetProducts()
        {
            return _db.Products.ToList();
        }
    }
}
