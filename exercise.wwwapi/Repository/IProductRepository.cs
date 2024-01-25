using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(string category = null);
        Product CreateProduct(Product product);
        Product GetAProduct(int id);
        Product UpdateProduct(int id, ProductPut product);
        Product DeleteProduct(int id);
    }
}
