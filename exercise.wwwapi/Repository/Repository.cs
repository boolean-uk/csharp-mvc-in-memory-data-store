using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {

        private DataContext db; 

        public Repository(DataContext db)
        {
            this.db = db;
        }

        public Product AddProduct(Product product)
        {
            //int id = 1; 
            //int id = db.Products.Max(x => x.Id) + 1;
            //product.Id = id;
            db.Add(product);
            db.SaveChanges();
            return product;
        }

        public Product DeleteProduct(int id)
        {
            var productToDelete = db.Products.FirstOrDefault(x => x.Id == id);
            db.Products.Remove(productToDelete);
            db.SaveChanges();
            return productToDelete; 
        }

        public Product GetProduct(int id)
        {
            var foundProduct = db.Products.FirstOrDefault(x => x.Id ==  id);
            return foundProduct; 

        }

        public IEnumerable<Product> GetProducts()
        {
            return db.Products.ToList();
        }

        public Product UpdateProduct(int id, string newName, string newCategory, int newPrice)
        {
            var productToUpdate = db.Products.FirstOrDefault(x =>x.Id == id);
            productToUpdate.Name = newName; 
            productToUpdate.Category = newCategory;
            productToUpdate.Price = newPrice;
            db.SaveChanges();
            return productToUpdate;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            var productsByCategory = db.Products.Where(x => x.Category == category).ToList();
            return productsByCategory;
        }

        public Product GetProductByName(string name)
        {
            var foundProduct = db.Products.FirstOrDefault(x => x.Name == name);
            return foundProduct; 
        }
    }
}
