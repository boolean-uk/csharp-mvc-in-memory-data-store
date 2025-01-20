using System.Threading.Tasks;
using exercise.wwwapi.Model;
using exercise.wwwapi.View;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(string category);
        Task<Product?> GetProduct(int id);
        Task<Product?> GetProduct(string name);
        Task<Product?> UpdateProduct(int id, Product_edit dto);
        Task<Product?> DeleteProduct(int id);
        Task<Product?> CreateProduct(Product_create dto);
    }
}
