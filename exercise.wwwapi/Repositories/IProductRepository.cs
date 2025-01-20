using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<Product> UpdateProduct(Product product);
        Task<bool> Delete(int id);
        Task<Product> AddProduct(Product product);
        Task<IEnumerable<Product>> GetProductsByCategory(string category);
    }
}
