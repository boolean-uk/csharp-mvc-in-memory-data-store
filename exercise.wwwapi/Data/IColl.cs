using System.Collections.Generic;

namespace exercise.wwwapi.Data
{
    public interface IColl<T>
    {
        T Insert(T entity);
        IEnumerable<T> GetAll();
        T GetById(object id);
        T Remove(object id);
        T Update(T entity);
    }
}
