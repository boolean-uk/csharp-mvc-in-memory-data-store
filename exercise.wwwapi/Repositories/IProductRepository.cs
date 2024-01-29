using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repositories
{
    public interface IProductRepository
    {
        public Task<List<Product>> getAllProducts();
        public Task<Product?> getProductById(int id);
        public Task<Product> AddProduct(ProductPostPayload payload);
        public Task<Product> UpdateProduct(int id, ProductPutPayload payload);
        public Task<bool> DeleteProduct(int id);
    }
}
