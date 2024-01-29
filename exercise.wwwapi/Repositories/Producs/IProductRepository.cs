using exercise.wwwapi.Models.Products;

namespace exercise.wwwapi.Repositories.Producs
{
    public interface IProductRepository
    {
        public Task<List<Product>> getAllProducts();
        public Task<Product?> getProductById(int id);
        public Task<Product> AddProduct(ProductPostPayload payload);
        public Task<Product> UpdateProduct(int id, ProductPutPayload payload);
        public Task<bool> DeleteProduct(int id);

        // discounts
        Task<bool> AttachDiscountToProduct(int product_id, int discount_id);
        Task<bool> RemoveDiscountFromProduct(int product_id);
    }
}
