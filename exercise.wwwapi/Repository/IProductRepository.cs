
using exercise.wwwapi.Models;


namespace exercise.wwwapi.Repository {

    public interface IProductRepository {

        public List<Product> GetAllProducts();
        public Product AddProduct(string Name, string Category, int Price);

        public Product? GetProduct(int Id);

        public Product? DeleteProduct(int Id);
        public Product? UpdateProduct(int Id, ProductUpdatePayload updateData);
    }
}