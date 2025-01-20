using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repositories
{
    public interface IRepository
    {
        Task<IEnumerable<Product>> GetProducts(string category);
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product, int id);
        Task<Product> DeleteProduct(int id);
    }
}
