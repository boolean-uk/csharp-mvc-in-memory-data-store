namespace exercise.wwwapi.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        List<T> GetAll(string category);
        T GetById(string Id);
        T GetByName(string name);
        T Update(string Id, T entity);
        T Delete(string Id);
    }
}
