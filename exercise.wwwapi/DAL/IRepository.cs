using exercise.wwwapi.Model;

namespace exercise.wwwapi.DAL
{
    public interface IRepository
    {
        Product Add(Product product);
        List<Product> GetProducts(string? category);
        Product Get(int id);
        Product Update(Product product, int id);
        Task<Product> Delete(int id);
    }
}
