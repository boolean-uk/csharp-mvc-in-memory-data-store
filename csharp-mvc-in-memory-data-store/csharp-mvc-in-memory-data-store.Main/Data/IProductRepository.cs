using mvc_in_memory_data_store.Models;

namespace mvc_in_memory_data_store.Data
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        Product AddProduct(Product product);
        Product DeleteProduct(int id);
        Product UpdateProduct(int id, Product updatedProduct);
    }
}