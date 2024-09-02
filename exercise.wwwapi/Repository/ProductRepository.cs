using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private DataContext _db;
        public ProductRepository(DataContext db)
        {
            _db = db;
        }

        public List<Product> GetAll(string? category)
        {
            if (category != null)
            {
                List<Product> result = _db.Products.Where(p => p.category == category).ToList();
                return result.Any() ? result : null;
            }
            return _db.Products.ToList();
        }

        public Product GetOne(int id)
        {
            return _db.Products.Find(id);
        }
        public Product Add(Product entity)
        {
            _db.Add(entity);
            _db.SaveChanges();

            return entity;
        }

        public Product Update(int id, Product entity)
        {
            var toChange = _db.Products.Find(id);
            if ( toChange != null)
            {
                entity.id = id;
                _db.Entry(toChange).CurrentValues.SetValues(entity);
                _db.SaveChanges();
            }
            return toChange;
        }

        public Product Delete(int id)
        { 
            var toDelete = _db.Products.Find(id);
            if (toDelete != null)
            {
                _db.Products.Remove(toDelete);
                _db.SaveChanges();
            }
            return toDelete;
        }
    }
}
