using exercise.wwwapi.Model.Data;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Model.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DataContext db;
        private DbSet<T> table = null;

        public Repository(DataContext dataContext)
        {
            this.db = dataContext;
            this.table = db.Set<T>();
        }

        public T Delete(object id)
        {
            T entity = this.table.Find(id);
            table.Remove(entity);
            db.SaveChanges();
            return entity;
        }

        public T Get(object id)
        {
            return table.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T Insert(T entity)
        {
            table.Add(entity);
            db.SaveChanges();
            return entity;
        }

        public T Update(object id, T entity)
        {
            table.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
            return entity;
        }
    }
}
