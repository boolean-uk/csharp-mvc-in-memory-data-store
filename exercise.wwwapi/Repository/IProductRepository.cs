
using exercise.wwwapi.Models;


namespace exercise.wwwapi.Repository {

    public interface IProductRepository {

        public Task<List<Product>> GetAllProducts();
        public Task<Product> AddProduct(string Name, string Category, int Price);

        public Task<Product> AddDiscountOnProduct(DiscountUpdatePayload paload);

        public Task<Product> GetProduct(int Id);

        public Task<Discount> AddDiscount(int Price);

        public Task<Product> DeleteProduct(int Id);
        public Task<Product> UpdateProduct(int Id, ProductUpdatePayload updateData);
    }
}