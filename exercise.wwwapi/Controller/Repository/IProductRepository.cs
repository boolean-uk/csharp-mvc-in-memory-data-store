using exercise.wwwapi.Model.Models;

namespace exercise.wwwapi.Controller.Repository
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
    }
}
