using exercise.wwwapi.Controllers.Models;
using exercise.wwwapi.Controllers.Models.DTO;

namespace exercise.wwwapi.Controllers.Repository
{
    public interface IProductRepository<T>
    {
        void create(string name, string category, int price);
        List<Product> getAll(string category);
        List<Product> getAll();
        Product find(int id);
        bool Add(Product product);
        void Delete(int id);
        Product Update(int id, ProductPut product);
    }
}
