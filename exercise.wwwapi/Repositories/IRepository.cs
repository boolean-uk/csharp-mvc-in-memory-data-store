namespace exercise.wwwapi.Repositories;

public interface IRepository<T> where T : class
{
    public T Add(T entity);
    public List<T> GetAll();
    public List<T> GetAll(string category);
    public T Get(int id);
    public T Update(T entityA, T entityB);
    public T Delete(T entity);
}