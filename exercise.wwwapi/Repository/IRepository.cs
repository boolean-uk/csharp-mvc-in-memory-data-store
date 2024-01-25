using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        IEnumerable<Product> GetProducts();
        Product AddProduct(Product product);
        Product GetProduct(int id);
        Product UpdateProduct(int id, string newName, string newCategory, int newPrice);
        Product DeleteProduct(int id);
        IEnumerable<Product> GetProductsByCategory(string category);
        Product GetProductByName(string name);
    }
}
