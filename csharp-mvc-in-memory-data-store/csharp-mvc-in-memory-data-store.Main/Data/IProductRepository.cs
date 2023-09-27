using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
    public interface IProductRepository
    {
        void create(String name, string category, int price);       
        List<Product> findAll();     
        Product find(int id);
        bool Add(Product product);
        void Delete(int id);
    }
}
