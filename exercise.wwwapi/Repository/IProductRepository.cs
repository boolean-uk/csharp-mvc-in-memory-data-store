using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetAllProducts(string? filter);

        public Product AddProduct(string name, string category, int price);

        public Product? GetProduct(int id);

        public Product? UpdateProduct(int id, ProductUpdatePayload updateData);

        public bool DeleteProduct(int id);
    }
}
