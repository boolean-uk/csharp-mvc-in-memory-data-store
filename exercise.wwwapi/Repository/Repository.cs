using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using static exercise.wwwapi.Model.@enum;

namespace exercise.wwwapi.Repository
{
    // Repository class - takes care of doing eiher a create, read, update or delete in the database and lets the controller know when it's done
    // This repo - implementation of different CRUD methods
    public class Repository : IRepository
    {
        private DataContext _database;

        public Repository(DataContext database)
        {
            _database = database;
        }
        public Product CreateProduct(Product product)
        {
            var newProduct = new Product()
            {
                name = product.name,
                price = product.price,
                type = product.type,
                id = product.id
            };
            _database.Products.Add(newProduct);
            _database.SaveChanges();
            return newProduct;
        }

        public Product DeleteProductById(int id)
        {
            var product = _database.Products.FirstOrDefault(p => p.id == id);
            _database.Products.Remove(product);
            _database.SaveChanges();
            return product;
        }

        public IEnumerable<Product> GetAllProducts(ProductType type)
        {
            return _database.Products.Where(p => p.type == type).ToList();
        }

        public Product GetProductById(int id)
        {
            var product = _database.Products.FirstOrDefault(p => p.id == id);
            return product;
        }

        public bool ProductExist(string name)
        {
            return true ? _database.Products.Any(p => p.name == name) : false;  
        }

        public bool ProductExist(int id)
        {
            return true ? _database.Products.Any(p => p.id == id) : false;
        }

        public Product UpdateProductById(int id, ProductPut productToUpdate)
        {
            var product = _database.Products.FirstOrDefault(p => p.id == id);
            if (product != null)
            {
                product.name = productToUpdate.name == "string" ? product.name : productToUpdate.name;
                product.price = productToUpdate.price == 0 ? product.price : productToUpdate.price;
                _database.SaveChanges();
            }
            return product;
        }
    }
}
