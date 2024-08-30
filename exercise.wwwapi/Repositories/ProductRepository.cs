using exercise.wwwapi.Data;
using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repositories
{
    public class ProductRepository : IRepository
    {
        DataContext _db;
        public ProductRepository(DataContext db)
        {
            this._db = db;
        }

        public Product Add(Product product)
        {
            _db.Add(product);
            _db.SaveChanges();
            return product;
        }

        public void Delete(int id)
        {
            Product product = _db.Products.FirstOrDefault(p => p.Id == id);
            _db.Remove(product);
            _db.SaveChanges();
        }

        public Product GetProduct(int id)
        {
            return _db.Products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProducts(string category)
        {
            return _db.Products.Where(p => p.Category == category).ToList();  
        }

        public Product Update(int id, Product newValues)
        {
            Product oldProduct = _db.Products.FirstOrDefault(x => x.Id == id);
            oldProduct.Name = newValues.Name;
            oldProduct.Category = newValues.Category;
            oldProduct.Price = newValues.Price;

            _db.SaveChanges();

            return oldProduct;
        }
    }
}
