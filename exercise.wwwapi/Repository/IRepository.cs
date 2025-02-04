using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<Product> AddProduct(Product model);

        Task<Product> UpdateProduct(int id, ProductPut model);
        Task<bool> DeleteProduct(int id);
    }
}
