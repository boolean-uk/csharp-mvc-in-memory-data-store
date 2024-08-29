using exercise.wwwapi.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace exercise.wwwapi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ProductDbContext _context;
        private readonly DbSet<T> _dbSet;


        public Repository(ProductDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public List<T> GetAll()
        {
            var products = _dbSet.ToList();
            return products;
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public T Update(int id, T entity)
        {
            var existingEntity = _dbSet.Find(id);
            if (existingEntity == null)
                return null;

            _dbSet.Entry(existingEntity).CurrentValues.SetValues(entity);

            _context.SaveChanges();

            var updatedEntity = _dbSet.Find(id);

            return updatedEntity;
        }

        public T Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
                return null;

            _dbSet.Remove(entity);
            _context.SaveChanges();

            return entity;
        }
    }
}
