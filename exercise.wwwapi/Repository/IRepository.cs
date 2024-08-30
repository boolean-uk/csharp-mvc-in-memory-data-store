using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        List<Product> GetProducts(string category);
        Product CreateProduct(Payload payload);
        Product GetProduct(int id);
        Product UpdateProduct(Payload payload, int id);
        Product DeleteProduct(int id);
        bool ContainsProduct(string name);
        bool ContainsProduct(int id);
    }
}
