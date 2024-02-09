using exercise.wwwapi.Model;

public interface IRepository
{

    IEnumerable<Product> GetAllProducts(string category);
    IEnumerable<Product> GetAllProducts();
    Product GetProductByID(int id);
    Product? AddProduct(Product product);
    Product? DeleteProduct(int id);
    Product? UpdateProduct(int id, ProductPut model);
}