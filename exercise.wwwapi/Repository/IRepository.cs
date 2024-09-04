using exercise.wwwapi.Model;
using exercise.wwwapi.View_Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        List<Product> GetProducts();

        Product AddProduct(Product product);

        Product GetProduct(int id);

        Product ChangeProduct(ProductPostModel product, int id);

        string DeleteProduct(int id);
        bool ContainsProduct(string name);
        bool ContainsProduct(int id);
        bool IsEmpty();

    }
}
