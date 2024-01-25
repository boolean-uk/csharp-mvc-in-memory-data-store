using exercise.wwwapi.Products;

namespace exercise.wwwapi.Data
{
    public interface IProduct
    {
        IEnumerable<Product> GetProducts(); 
        Product Update(int id, ProductPut product);
        bool Get(int id, out Product product);
        Product Add(Product product);
        bool Delete(int id);
    }
}
