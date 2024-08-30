using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repositories
{
    public interface IRepository
    {
        Product Add(Product product);
        List<Product> GetProducts(string category);
        Product GetProduct(int id);
        Product Update(int id, Product newValues);
        void Delete(int id);
    }
}
