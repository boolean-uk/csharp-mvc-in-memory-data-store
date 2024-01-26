namespace exercise.wwwapi.Model.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(object id);
        T Insert(T entity);
        T Update(object id, T entity);
        T Delete(object id);
    }
}
