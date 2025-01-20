using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Task<Product> Get(int id);
        Task<IEnumerable<Product>> GetAll(string category = "");
        Task<IEnumerable<Product>> Delete(int id);
        Task<IEnumerable<Product>> Update(int id, string name, string category, int price);
        Task<IEnumerable<Product>> Add(Product entity);
    }
}
