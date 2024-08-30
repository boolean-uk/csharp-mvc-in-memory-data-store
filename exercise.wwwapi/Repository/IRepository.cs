using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        List<Product> GetProducts(string category);
        Product AddProduct(Product entity);
        Product GetProduct(int id);
        Product UpdateProduct(int id, Product product);
        Product DeleteProduct(int id);
    }
}
