using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetAllProducts();

        public Product AddProduct(string name, string catagory, int price);

        public Product? GetProduct(int id);

        public Product? UpdateProduct(Product book, ProductUpdatePayload updateData);

        public bool DeleteProduct(int id);
    }
}
