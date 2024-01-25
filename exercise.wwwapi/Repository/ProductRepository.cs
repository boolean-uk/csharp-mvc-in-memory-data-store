using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository
    {
        private DataContext _db;
        public ProductRepository(DataContext context) 
        { 
            _db = context;
        }
        public Product AddProduct(PostProduct product)
        {
            Product productToAdd = new Product() 
            {
                name = product.name,
                category = product.category,
                price = product.price,
            };
            _db.Products.Add(productToAdd);
            _db.SaveChanges();
            return productToAdd;
        }

        public Product DeleteProduct(int id)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _db.Remove(id);
                _db.SaveChanges();
            }
            return product;

        }

        public IEnumerable<Product> GetAllProducts(string? category)
        {
            if (category == null)
            {
                return _db.Products.ToList();
            }
            return _db.Products.Where(x => x.category == category).ToList();
        }

        public Product GetProduct(int id)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == id);
            return product;
        }

        public Product UpdateProduct(int id, PutProduct product)
        {
            var productToUpdate = _db.Products.FirstOrDefault(x => x.Id == id);
            if(productToUpdate != null)
            {
                productToUpdate.category = product.category;
                productToUpdate.price = (int)product.price;
                productToUpdate.name = product.name;
                _db.SaveChanges();

            }
            return productToUpdate;
        }
    }
}
