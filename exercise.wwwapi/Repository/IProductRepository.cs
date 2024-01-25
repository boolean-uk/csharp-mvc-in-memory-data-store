using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(string? category);
        IEnumerable<Product> GetAllProducts();
        Product AddProduct(Product product);
        
        Product UpdateProduct(int id, ProductPut productPut);
        Product GetAProduct(int id);
        Product DeleteProduct(int id);
    }
}
