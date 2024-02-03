using exercise.wwwapi.Models;
using exercise.wwwapi.Data;


namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {

        private DatabaseContext _db;
        public Repository(DatabaseContext db)
        {
            _db = db;
        }

        // add a product
        public Product CreateProduct(Product product)
        {
            _db.Add(product);
            _db.SaveChanges();
            return product;
        }

        // get all products
        public IEnumerable<Product> GetProducts()
        {
            return _db.Product.ToList();
        }

        // get a product

        public Product GetProduct(int id)
        {
            return _db.Product.FirstOrDefault(product => product.Id == id);
        }

        // update a product
        public Product UpdateProduct(int id, ProductPut productPut)
        {

            var found = _db.Product.FirstOrDefault(p => p.Id == id);
            if (found == null)
            {
                return null;
            }

            found.Name = productPut.Name;
            found.Category = productPut.Category;
            found.Price = productPut.Price;
            _db.SaveChanges();
            return found;
        }

        // delete a product
        public Product DeleteProduct(int id)
        {
            var found = _db.Product.FirstOrDefault(p => p.Id == id);
            if (found == null)
            {
                return null;

            }

            _db.Product.Remove(found);
            _db.SaveChanges();
            return found;
        }
    }
    
}
