using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository<T> where T : class
    {
        T Create(T product);
        
        T Get(int id);

        T Update(T entity, int id);

        T Delete(int id);
        List<Product> GetAllProducts(string category);
    }
}
