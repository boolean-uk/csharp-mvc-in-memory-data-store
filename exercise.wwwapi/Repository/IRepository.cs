using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProducts(string category);
        Product AddProduct(Product product);
        Product UpdateProduct(int id, ProductPut product);
        Product GetAProduct(int id);
        Product DeleteProduct(int id);
    }
}
