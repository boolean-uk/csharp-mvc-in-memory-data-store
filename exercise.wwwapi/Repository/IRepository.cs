using exercise.wwwapi.Models;
using exercise.wwwapi.ViewModel;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Product>> GetProducts(string category);
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProduct(int id);
        Task<Product> GetProductByName(string name);
        Task<Product> DeleteProduct(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product, int id);
    }
}
