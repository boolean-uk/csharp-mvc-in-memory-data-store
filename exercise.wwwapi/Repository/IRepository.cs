using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository<T>
    {
        T Insert(T entity);
        IEnumerable<T> Get();
        T Update(T entity);
        T Delete(object id);
        T GetById(object id);
    }
}
