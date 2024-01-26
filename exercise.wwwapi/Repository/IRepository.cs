namespace exercise.wwwapi.Repository
{
    public interface IRepository<T>
    {
        T Insert(T entity);
        IEnumerable<T> Get();
        T GetById(int id);
        T Update(T updatedEntity); // Method for updating an entity
        T Delete(int id); // Method for deleting an entity
    }
}
