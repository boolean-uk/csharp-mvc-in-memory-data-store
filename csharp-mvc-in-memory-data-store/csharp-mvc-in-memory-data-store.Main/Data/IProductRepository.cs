using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
    public interface IProductRepository
    {
        Product Create(string name, string category, int price);       
        List<Product> FindAll();     
        Product FindById(int id);
        Product FindByName(string name);
        bool Add(Product product);
        bool Delete(Product product);
    }
}
