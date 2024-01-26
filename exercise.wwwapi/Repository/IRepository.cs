using exercise.wwwapi.Model;
using static exercise.wwwapi.Model.@enum;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Product CreateProduct(Product product);
        IEnumerable<Product> GetAllProducts(ProductType type);
        Product GetProductById(int id);
        Product UpdateProductById(int id, ProductPut productToUpdate);
        Product DeleteProductById(int id);
        bool ProductExist(string name);
        bool ProductExist(int id);
    }
}
