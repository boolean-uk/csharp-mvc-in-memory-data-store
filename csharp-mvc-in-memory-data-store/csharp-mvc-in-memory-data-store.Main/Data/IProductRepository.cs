using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
    public interface IProductRepository
    {
        //void create(string name,string category, int price);       
        List<Product> GetAll();     
        Product GetById(int id);
        bool AddProduct(Product product);
        bool ChangeById(int id, Product product);
        Product RemoveById(int id);
    }
}
