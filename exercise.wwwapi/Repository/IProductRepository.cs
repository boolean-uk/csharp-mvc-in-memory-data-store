using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {

        Task<Product> AddProduct(Product product);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> UpdateProduct(int id, string name, string category, int price);
        Task<Product> DeleteProductById(int id);
        Task<bool> Contains(int id);
    }
}

