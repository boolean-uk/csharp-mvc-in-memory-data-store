using exercise.wwwapi.Data;
using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private DataContext _db;
        public Repository(DataContext db)
        {
            _db = db;
        }

        public IEnumerable<Product> GetAllProducts(string category)
        {
            return string.IsNullOrEmpty(category) ? _db.products.ToList() : _db.products.Where(p => p.Category == category).ToList();
        }

        public Product GetAProduct(int id)
        {
            var product = _db.products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            return product;
        }

        public Product AddProduct(InPuProduct product)
        {
            
            if (_db.products.Any(p => p.Name == product.Name))
            {
                return null;
            }
            Product newProduct = new Product();
            newProduct.Id = (_db.products.Count() == 0 ? 0 : _db.products.Max(book => book.Id) + 1);
            newProduct.Name = product.Name;
            newProduct.Category = product.Category;
            newProduct.Price = int.Parse(product.Price);
            _db.products.Add(newProduct);
            _db.SaveChanges();
            return newProduct;
        }

        public Product UpdateAProduct(int id, InPuProduct product)
        {
            var updatedProduct = _db.products.FirstOrDefault(p => p.Id == id);
            if (updatedProduct == null)
            {
                return null;
            }
            updatedProduct.Name = product.Name;
            updatedProduct.Category = product.Category;
            updatedProduct.Price = int.Parse(product.Price);
            _db.SaveChanges();
            return updatedProduct;
        }

        public Product DeleteABook(int id)
        {
            var product = _db.products.FirstOrDefault(p => p.Id == id);
            _db.products.Remove(product);
            _db.SaveChanges();
            return product;
        }
    }
}
