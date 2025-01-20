using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Task<Product> AddProduct(Product product);
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<bool> UpdateProduct(int id);
        Task<bool> DeleteProduct(int id);
    }
}
