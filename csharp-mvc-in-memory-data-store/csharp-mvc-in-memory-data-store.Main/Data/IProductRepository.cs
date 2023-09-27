using mvc_in_memory_data_store.Models;


namespace mvc_in_memory_data_store.Data
{
    public interface IProductRepository
    {
        Product Add(Product product);
        List<Product> FindAll();
        Product FindById(int id);
        Product Update(int id, Product product);
        Product Delete(int id);
        List<Product> FindByCategory(string category);
        bool ProductNameExists(string Name);
    }
}
