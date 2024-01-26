using exercise.wwwapi.Models;
using System.ComponentModel;

namespace exercise.wwwapi.Repository
{
    public interface IRepository<T>
    {
        T Add(T entity);
        IEnumerable<T> Get();
        T GetById(object id);
        T Update(T entity);
        T Delete(object id);
        void Save();
    }
}
