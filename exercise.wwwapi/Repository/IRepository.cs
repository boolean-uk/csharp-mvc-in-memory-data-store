using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Product Add(ProductParameters product);
        Product Update(Product product, ProductParameters newData);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAll(string category);
        Product Get(int id);
        Product Delete(Product product);

    }
}
