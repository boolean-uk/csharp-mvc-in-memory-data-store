using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Repositories
{
    public interface IProductRepository
    {
        void create(int Id, string Name, string Category, decimal Price);
        List<Product> findAll();
        Product find(int Id);
        bool Add(Product product);
        bool GetProduct(int id, out Product? product);


    }
}
