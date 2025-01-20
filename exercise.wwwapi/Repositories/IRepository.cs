namespace exercise.wwwapi.Repositories;

public interface IRepository<T> where T : class
{
    // CRUD operations
    T Create(T entity);
    T? Read(Guid id);
    T Update(T entity);
    void Delete(Guid id);
    
    // Also part of "Create", just better
    T? GetByName(string name);
    List<T> GetAll();
    List<T> GetAll(string category);

    
    // Idk why I need this
    void Detach(T entity);
}