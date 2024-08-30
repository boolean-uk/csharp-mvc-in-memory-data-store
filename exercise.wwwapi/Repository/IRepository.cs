using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        //list all Product Methods
        List<Product> GetAllProducts(string category);
        Product AddProduct(Product entity);
        Product GetAProduct(int Id);
        Product UpdateProduct(Product newProduct, int Id);
        Product DeleteProduct(int Id);

    }
}
