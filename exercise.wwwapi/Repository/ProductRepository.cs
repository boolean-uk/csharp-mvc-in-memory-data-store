using exercise.wwwapi.Data;
using exercise.wwwapi.Model;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private ProductDataContext _db;
        private DbSet<Product> _dbSet;

        public ProductRepository(ProductDataContext db)
        {
            _db = db;
            _dbSet = _db.Set<Product>();
        }

        public Product Create(Product entity)
        {
            if (_dbSet.Any(x => x.Name == entity.Name)) return null;

            entity.ID = _dbSet.Max(x => x.ID);

            _dbSet.Add(entity);
            _db.SaveChanges();

            return entity;
        }

        public Product Delete(int id)
        {
            var entity = _dbSet.FirstOrDefault(product => product.ID == id);
            _dbSet.Remove(entity);
            _db.SaveChanges();

            return entity;
        }

        public Product Get(int? id)
        {
            return _dbSet.FirstOrDefault(product => product.ID == id);
        }

        public List<Product> GetAll()
        {
            return _dbSet.ToList();
        }

        public Product Update(int id, Product entity)
        {
            var existingEntity = _dbSet.Find(id);
            entity.ID = existingEntity.ID;

            _dbSet.Entry(existingEntity).CurrentValues.SetValues(entity);
            _db.SaveChanges();

            var updatedEntity = _dbSet.Find(id);
            return updatedEntity;
        }
    }
}
