namespace exercise.wwwapi.repository;

public interface IRepository<T, U>
{
    public Task<IEnumerable<T>> GetAll();
    public Task<IEnumerable<T>?> GetSome(Predicate<T> pred);
    public Task<T?> Get(int id);
    public Task<T?> Delete(int id);
    public Task<T> Add(T entity);
    public Task<T?> Update(int id, U partialEntity);
}
