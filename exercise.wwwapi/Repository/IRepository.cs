using exercise.wwwapi.Products;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        IEnumerable<Product> GetProducts(); 
        Product Add(Product product);
        Product Update(int id, ProductPut productPut);

        bool Delete(int id);

        
    }
}
