using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T>? Get();
        T? Get(int id);
        T? Create(T product);
        T? Update(int id, T product);
        T? Delete(int id);

        bool NameExists(string name);
    }
}
