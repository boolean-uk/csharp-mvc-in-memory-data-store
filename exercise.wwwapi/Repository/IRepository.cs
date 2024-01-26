using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Product CreateProduct(Product product);
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        Product UpdateProductById(int id, ProductPut productToUpdate);
        Product DeleteProductById(int id);
    }
}
