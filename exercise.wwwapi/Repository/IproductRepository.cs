using exercise.wwwapi.Data;
using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    //Interface for productRepository
    public interface IproductRepository
    {
        public Task<Product> CreateProduct(ProductPayload CreateData);

        public Task<IEnumerable<Product>> GetAllProducts();

        public Task<IEnumerable<Product>> GetProductsByCategory(string category);

        public Task<Product> GetProductById(int id);

        public Task<Product> UpdateProduct(int id, ProductPayload updateData);

        public Task<Product> DeleteProduct(int id);
    }
}
