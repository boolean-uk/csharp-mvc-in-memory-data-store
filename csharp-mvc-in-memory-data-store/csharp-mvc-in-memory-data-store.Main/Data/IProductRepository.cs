using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
    public interface IProductRepository
    {
        void create(string name, string category, decimal price);
        List<Product> findAll();
        Product find(int id);
        bool Add(Product product);
        Product Update(int id, string name, string category, decimal price);
        bool Delete(int id);
    }
}
