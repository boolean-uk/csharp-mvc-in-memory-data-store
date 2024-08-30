using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository
    {

        private DataContext _db;

        public ProductRepository(DataContext db)
        {
            _db = db;
        }
        public Product AddProduct(Product entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
            return entity;
        }

        public List<Product> GetAllProducts(string category)
        {
            return _db.Products.Where(prod => prod.Category == category).ToList();
        }

        public Product GetAProduct(int Id)
        {
            return _db.Products.FirstOrDefault(prod => prod.Id ==  Id);
        }

        public Product UpdateProduct(Product newProduct, int Id)
        {
            var productToBeUpdated = GetAProduct(Id);

            productToBeUpdated.Name = newProduct.Name;
            productToBeUpdated.Category = newProduct.Category;
            productToBeUpdated.Price = newProduct.Price;

            _db.SaveChanges();

            return productToBeUpdated;
        }

        public Product DeleteProduct(int Id)
        {
            Product productToBeDeleted = GetAProduct(Id);
            _db.Products.Remove(productToBeDeleted);
            _db.SaveChanges();
            return productToBeDeleted;
        }
    }
}
