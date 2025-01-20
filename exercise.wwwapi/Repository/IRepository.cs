using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {

        Task<IEnumerable<Product>> GetAllProducts(string category);
        Task<Product> GetProduct(int id);
        Task<bool> DeleteProduct(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(int id, Product updatedproduct);

        //support-method for 400 response "The provided name already exists":
        Task<Product> GetProductByName(string name);

    }
}
