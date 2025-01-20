using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Product>> GetAll(string category);
        Task<Product> GetById(int id);
        Task<Product> GetByName(string name);
        Task<Product> Delete(int id);
        Task<Product> Add(Product product);
        Task<Product> Update(Product product);

    }
}
