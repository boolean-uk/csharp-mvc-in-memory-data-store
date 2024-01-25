using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    // Interface for ProductRepository with methods
    public interface IProductRepository
    {
        public List<Product> GetAllProducts();
        
        public Product? GetProduct(int id); 

        public Product AddProduct(string name, string category, int price);

        public Product? UpdateProduct(int id, ProductUpdatePayload productUpdatePayload);

        public Product? DeleteProduct(int id);

    }
}