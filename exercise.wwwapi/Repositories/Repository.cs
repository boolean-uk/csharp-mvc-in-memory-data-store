using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repositories
{
    public class Repository : IRepository
    {
        private DataContext _db;
        public Repository(DataContext db)
        {
            _db = db;
        }
        public List<Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }
        public Product AddProduct(Product product)
        {
            _db.Add(product);
            _db.SaveChanges();
            return product;
        }

        public List<Product> GetProducts(string category)
        {
            return _db.Products.Where(p => p.Category == category).ToList();
        }

        public Product GetSingleProduct(int id)
        {
            return _db.Products.Find(id);
        }

        public Product RemoveProduct(Product product)
        {
            _db.Products.Remove(product);
            _db.SaveChanges();
            return product;
        }

        public Product UpdateProduct(int id, Product product)
        {
            Product productToBeUpdated = GetSingleProduct(id);

            if (productToBeUpdated == null)
            {
                return null;
            }

            if (ProductExists(product.Name))
            {
                foreach(var p in GetProductsWithName(product.Name))
                {
                    if (p.Id != product.Id)
                    {
                        return null;
                    }
                }
            }

            productToBeUpdated.Name = product.Name;
            productToBeUpdated.Category = product.Category;
            productToBeUpdated.Price = product.Price;
            _db.SaveChanges();
            return productToBeUpdated;
        }

        public bool ProductExists(string productName)
        {
            List<Product> productList = _db.Products.Where(p => p.Name == productName).ToList();
            if(productList.Count > 0)
            {
                return true;
            }
            return false;
        }

        public List<Product> GetProductsWithName(string name)
        {
            return _db.Products.Where(p => p.Name == name).ToList();
        }
    }
}
