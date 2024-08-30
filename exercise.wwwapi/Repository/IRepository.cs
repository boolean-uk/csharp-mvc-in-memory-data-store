using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll(string? category);
        T Add(T entity);
        T Get(int id);
        T GetByName(string name);
        T Update(int id, T entity);
        T Delete(int id);

    }
}
