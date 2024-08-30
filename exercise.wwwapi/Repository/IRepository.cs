namespace exercise.wwwapi.Repository
{
    public interface IRepository<T> where T : class
    {
        T Create(T entity);
        List<T> GetAll();
        T Get(int? id);
        T Update(int id, T entity);
        T Delete(int id);
    }
}
