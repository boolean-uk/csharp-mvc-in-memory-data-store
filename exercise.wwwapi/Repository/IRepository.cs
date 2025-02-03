using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int id);
        Product AddProduct(ProductPost model);

        Product UpdateProduct(int id, ProductPut model);
        bool DeleteProduct(int id);

    }
}
