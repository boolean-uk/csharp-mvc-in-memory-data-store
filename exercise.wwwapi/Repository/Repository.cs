using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private DataContext _db;

        public Repository(DataContext db)
        {
            _db = db;
        }

        public Product AddProduct(UserProduct userProduct)
        {
            Product product = new Product()
            {
                name = userProduct.name,
                category = userProduct.category,
                price = userProduct.price
            };
            
            _db.Products.Add(product); 
            _db.SaveChanges();
            return product;
        }

        public Product GetProduct(int id)
        {
            return _db.Products.FirstOrDefault(x => x.id == id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _db.Products.ToList();
        }

        public IEnumerable<Product> GetProducts(string? category)
        {
            if ( category == null) { return GetProducts(); }
            return _db.Products.Where(x => x.category == category).ToList();
        }

        public Product UpdateProduct(Product product, UserProduct userProduct)
        {
            return product.Update(userProduct);
        }

        public Product RemoveProduct(Product product)
        {
            _db.Products.Remove(product);
            _db.SaveChanges(); 
            return product;
        }
    }
}
