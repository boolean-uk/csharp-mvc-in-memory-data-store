using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
    public interface IProductRepository
    {
        void Create(string name, string category, int price);       
        List<Product> FindAll();     
        Product FindById(int id);
        bool Add(Product product);
    }
}
