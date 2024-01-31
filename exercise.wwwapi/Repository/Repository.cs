
using exercise.wwwapi.Data;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class Repository<T> : IRepo<T> where T : class
    {

        private DataContext _db;
        private DbSet<T> _table = null;

        public Repository(DataContext db)
        {
            _db = db;
            _table = db.Set<T>();
        }
        public T Add(T item)
        {
            _table.Add(item);
            _db.SaveChanges();
            return item;
        }

        public T Delete(Func<T, bool> predicate)
        {
            var entity = _table.FirstOrDefault(predicate);
            if (entity != null)
            {
                _table.Remove(entity);
                _db.SaveChanges();
            }
            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public T GetSingle(Func<T, bool> predicate)
        {
            return _table.FirstOrDefault(predicate);
        }

        public T Update(T item)
        {
            _table.Attach(item);
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
        }
    }
}
