
using System.Collections.Generic;
using exercise.wwwapi.Data;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private Datacontext _db;
        private DbSet<T> _table = null;

        public Repository(Datacontext dataContext)
        {
            _db = dataContext;
            _table = _db.Set<T>();
        }

        public T Insert(T entity)
        {
            _table.Add(entity);
            _db.SaveChanges();
            return entity;
        }

        public IEnumerable<T> Get()
        {
            return _table.ToList();
        }

        public T GetById(int id)
        {
            return _table.Find(id);
        }

        public T Update(T updatedEntity)
        {
            _table.Add(updatedEntity);
            _db.Entry(updatedEntity).State = EntityState.Modified;
            _db.SaveChanges();
           

            return updatedEntity;
        }

        public T Delete(int id)
        {
            var entityToRemove = _table.Find(id);

            if (entityToRemove != null)
            {
                _table.Remove(entityToRemove);
                _db.SaveChanges();
            }

            return entityToRemove;
        }
    }
}
