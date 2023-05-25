using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
   public interface IProductRepository
    {
        Product Create(string name, string category, decimal price);
        List<Product> GetAll();
        Product Get(Guid id);
        bool Add(Product product);
        Product UpdateProduct(Product product);
        bool Delete(Guid id);

    }
}