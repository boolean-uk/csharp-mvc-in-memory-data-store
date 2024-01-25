using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository;

public interface IRepository
{
    Product AddProduct(PostProduct postProduct);
    IEnumerable<Product> GetProducts();
    IEnumerable<Product> GetProducts(string category);
    Product? GetProduct(int id);
    Product? UpdateProduct(int id, PutProduct postProduct);
    Product? DeleteProduct(int id);
    bool ProductExists(string name, out int existingId);
}
