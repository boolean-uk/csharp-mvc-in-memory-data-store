using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        IEnumerable<Product> GetAllProducts(string? category);
        Product GetProduct(int id);
        Product AddProduct(PostProduct product);
        Product UpdateProduct(int id, PutProduct product);
        Product DeleteProduct(int id);
    }
}
