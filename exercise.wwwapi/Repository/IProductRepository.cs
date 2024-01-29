using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllProducts();

        public Task<Product> AddProduct(string name, string catagory, int price);

        public Task<Product>? GetProduct(int id);

        public Task<Product>? UpdateProduct(Product book, ProductUpdatePayload updateData);

        public Task<bool> DeleteProduct(int id);

        public Task<ProductDeal> AddDealToTask(int id, string text);
    }
}
