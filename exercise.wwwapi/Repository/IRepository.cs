using exercise.wwwapi.Model;

public interface IRepository
{

    IEnumerable<Product> GetAllProducts();
    Product GetProductByID(int id);
    Product AddProduct(Product product);
    bool DeleteProduct(int id);
    bool UpdateProduct(int id, ProductPut model);
}