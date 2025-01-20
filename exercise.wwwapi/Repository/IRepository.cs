using exercise.wwwapi.DTO;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Task<Product> Add(Product entity);
        Task<Product> Update(Product entity);
        Task<Product> Delete(int id);
        Task<Product> Get(int id);
        Task<IEnumerable<Product>> GetAll(string category);
    }
}
