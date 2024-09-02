namespace exercise.wwwapi.Repository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll(string? query);
        T GetOne(int id);
        T Add(T entity);
        T Update(int id, T entity);
        T Delete(int id);
    }
}
