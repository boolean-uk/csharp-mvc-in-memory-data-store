using workshop.wwwapi.Models;
using workshop.wwwapi.ViewModel;

namespace workshop.wwwapi.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(string category);
        Task<Product> GetProduct(int id);
        Task<bool> DeleteProduct(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
    }
}
