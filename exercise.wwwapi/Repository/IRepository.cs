using exercise.wwwapi.Models;
using exercise.wwwapi.ViewModel;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Product>> GetAll(string category);
        Task<Product> Get(int id);
        Task<bool> Delete(int id);
        Task<Product> Add(Product entity);
        Task<Product> Update(Product entity);
        Task<bool> NameExists(string name, int id);
    }
}