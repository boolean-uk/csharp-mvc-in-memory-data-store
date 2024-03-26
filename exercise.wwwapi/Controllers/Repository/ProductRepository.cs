using exercise.wwwapi.Controllers.Models;
using exercise.wwwapi.Controllers.Models.DTO;
using exercise.wwwapi.Controllers.Data;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Controllers.Repository
{
    public class ProductRepository<T> : IProductRepository<T> where T : class
    {
        private static int IdCounter = 1;
        private static List<Product> _products = new List<Product>();
        private DataContext _db;
        //private DbSet<T> _products = null;

        public ProductRepository(DataContext datacontext)
        {
            _db = datacontext;
            //_products = _db.Set<T>();
        }

        public bool Add(Product product)
        {
            if (product != null)
            {
                int id = _products.Count == 0 ? 1 : _products.Max(x => x.id) + 1;
                product.id = id;
                _products.Add(product);
                return true;
            }
            return false;
        }

        public void create(string name, string category, int price)
        {
            Product product = new Product(ProductRepository<T>.IdCounter++, name, category, price);
            ProductRepository<T>._products.Add(product);
        }

        public void Delete(int id)
        {
            var item = find(id);
            if (item.id == id)
            {
                _products.Remove(item);
            }
        }

        public Product find(int id)
        {
            return ProductRepository<T>._products.First(product => product.getId() == id);
        }

        public List<Product> getAll()
        {
            return ProductRepository<T>._products;
        }

        public List<Product> getAll(string category)
        {
            return ProductRepository<T>._products.Where(x => x.category.ToLower() == category.ToLower()).ToList();
        }

        public Product Update(int id, ProductPut product)
        {
            if (ProductRepository<T>._products.Any(x => x.id == id))
            {
                Product productToUpdate = ProductRepository<T>._products.Where(x => x.id == id).First();
                productToUpdate.name = product.name;
                productToUpdate.category = product.category;
                productToUpdate.price = product.price;
                _db.SaveChanges();
                return productToUpdate;
            }
            else
            {

                return null;
            }
        }
    }
}
