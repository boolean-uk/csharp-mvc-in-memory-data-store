using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task <IEnumerable<Product>> GetProduct(string? category);
        Task<Product> GetProductName(string name);
        Task<Product> GetProductId(int id);
        Task<bool> Delete(int id);
        Task<Product> AddProduct(Product pet);

        Task<Product> UpdateProduct(Product pet);
    }
}
