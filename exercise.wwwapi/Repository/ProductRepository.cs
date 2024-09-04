using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository<Product>
    {

        private ProductDataContext _db;
        private DbSet<Product> _dbSet;

        public ProductRepository(ProductDataContext db, DbSet<Product> dbSet)
        {
            _db = db;
            _dbSet = dbSet;
        }

        public Product Create(Product product)
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetAllProducts(string Category)
        {
            if (string.IsNullOrWhiteSpace(Category))
            {
                return _dbSet.ToList();
            }
            else
            {
                return _dbSet.Where(x => x.Category == Category).ToList();
            }
        }

        public Product Delete(int id)
        {
            Product deletedproduct = _dbSet.FirstOrDefault(x => x.Id == id);
    
             _dbSet.Remove(deletedproduct);
             _db.SaveChanges();
            
            return deletedproduct;
        }

        public Product Update(Product entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}
