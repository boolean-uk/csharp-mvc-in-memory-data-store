using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Product>> GetAll(string? category);
        Task<Product> GetProduct(string id);
        Task<Product> CreateProduct(Product product);
        Task<bool> DeleteProduct(string id);
        void Save();
    }
}
