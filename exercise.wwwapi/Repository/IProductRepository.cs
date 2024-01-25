using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProducts(string category);
        Product AddProduct(Product product);

        Product UpdateProduct(int id, ProductPut productPut);

        Product GetAProduct(int id);

        Product DeleteProduct(int id);
       
    }
}
