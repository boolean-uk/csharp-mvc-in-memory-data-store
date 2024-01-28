using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository<T> : IRepository<T> where T : class
    {
        private DataContext _db;
        private DbSet<T> _table = null;

        public ProductRepository(DataContext db)
        {
            _db = db;
            _table = _db.Set<T>();
        }

        /// <inheritdoc/>
        public IEnumerable<T> Get()
        {
            return _table.ToList();
        }

        /// <inheritdoc/>
        public T Insert(T entity)
        {

            _table.Add(entity);
            _db.SaveChanges();
            return entity;

        }

        /// <inheritdoc/>
        public T Update(T entity)
        {
            _table.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return entity;
        }

        /// <inheritdoc/>
        public T? GetById(int id)
        {
            return _table.Find(id);
        }

        /// <inheritdoc/>
        public T? Delete(int id)
        {
            T? entity = _table.Find(id);
            if (entity == null) 
            {
                return entity;
            }

            _table.Remove(entity);
            _db.SaveChanges();

            return entity;
        }
    }
}
