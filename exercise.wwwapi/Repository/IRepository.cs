using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Product AddProduct(ProductBody product);
        List<Product> GetProducts(string? category);
        Product GetProduct(int id);
        Product UpdateProduct(int id, ProductBody product);
        void DeleteProduct(int id);
        bool NameExists(string name);
        bool IdExists(int id);
    }
}
