using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepo
    {
        List<Product> GetProducts(string category);
        Product GetProduct(int id);
        Product CreateProduct(Product product);
        Product UpdateProduct(Product product, int id);

        void DeleteProduct(int id);

    }
}
