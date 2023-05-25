using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
   public interface IProductRepository
    {
        Product Create(string name, string category, decimal price);
        List<Product> GetAll();
        Product Get(Guid id);
        Product UpdateProduct(Guid id, string name, string category, decimal price);
        bool Delete(Guid id);

    }
}