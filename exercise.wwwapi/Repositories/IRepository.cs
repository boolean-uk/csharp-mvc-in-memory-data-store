namespace exercise.wwwapi.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        List<T> GetAll();
        T Get(string Id);
        T Update(string Id, T entity);
        T Delete(string Id);
    }
}
