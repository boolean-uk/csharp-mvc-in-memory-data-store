using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        bool AddProduct(Product product);
        bool DeleteProduct(int id);
        bool UpdateProduct(Product product, int id);
    }
}
