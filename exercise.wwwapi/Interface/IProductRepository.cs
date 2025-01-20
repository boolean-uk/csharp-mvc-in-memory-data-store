using exercise.wwwapi.Model;

namespace exercise.wwwapi.Interface
{
    public interface IProductRepository
    {
        Task <IEnumerable<Product>> GetProducts();
        Task<Product> GetSingleProduct(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(int id, Product product);
        Task<bool> DeleteProduct(int id);
    }
}
