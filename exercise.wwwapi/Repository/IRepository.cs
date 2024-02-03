using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        IEnumerable<Product> GetProducts(); 
        Product GetProduct(int id);
        Product CreateProduct(Product product);
        Product UpdateProduct(int id, ProductPut productPut);

        Product DeleteProduct(int id);

        
    }
}
