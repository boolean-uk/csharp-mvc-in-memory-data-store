using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repositories
{
    public interface IProductRepository
    {
        public List<Product> getAllProducts();
        public Product getProductById(int id);
        public Product AddProduct(ProductPostPayload payload);
        public Product UpdateProduct(int id, ProductPutPayload payload);
        public bool DeleteProduct(int id);
    }
}
