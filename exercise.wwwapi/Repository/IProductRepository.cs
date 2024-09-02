using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        Product CreateProduct(Product product);

        List<Product> GetAllProducts(string category);

        Product GetProductByName(string name);


        Product GetProduct(int id);

        Product UpdateProduct(Product product);

        Product DeleteProduct(int id);
    }
}
