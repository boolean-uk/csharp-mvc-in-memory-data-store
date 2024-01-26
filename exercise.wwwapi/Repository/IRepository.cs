using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Product? AddProduct(Product product);
        IEnumerable<Product> GetProducts(string category);
        Product? GetAProduct(int id);
        Product? UpdateProduct(int id, ProductPost product);
        Product? DeleteProduct(int id);
    }
}
