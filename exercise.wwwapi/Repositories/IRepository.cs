using exercise.wwwapi.Model;
using exercise.wwwapi.ViewModel;

namespace exercise.wwwapi.Repositories
{
    public interface IRepository
    {

        List<Product> getAll();

        Product get(int id);

        bool checkIfExists(string name);

        Product update(int id, ProductViewModel product);

        Product delete(int id);

        Product create(Product product);

        string message { get; set; }




    }
}
