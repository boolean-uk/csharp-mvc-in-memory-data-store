using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Product>> GetAll(string category = "None");
        Task<Product> Get(int id);
        Task<bool> Delete(int id);
        Task<Product> Add(Product product);
    }
}
