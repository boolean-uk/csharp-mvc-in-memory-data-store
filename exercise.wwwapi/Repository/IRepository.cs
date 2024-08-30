using exercise.wwwapi.Models;
using exercise.wwwapi.ViewModels;
using System.Runtime.InteropServices;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        List<Product> GetAllProducts(string? category);

        Product CreateProduct(Product product);

        Product GetAProduct(int id);

        Product UpdateProduct(int id, Product product);

        Product DeleteProduct(int id);
    }
}
