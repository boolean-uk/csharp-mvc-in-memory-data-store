using exercise.wwwapi.Models;
using exercise.wwwapi.ViewModel;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Get(int id);
        Task<bool> Delete(int id);
        Task<Product> Add(Product entity);
        Task<Product> Update(int id, ProductViewModel entity);
    }
}