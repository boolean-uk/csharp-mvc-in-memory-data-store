using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    //Interface for productRepository
    public interface IproductRepository
    {
        public Product CreateProduct(string productName, string productCategory, int price);

        public List<Product> GetAllProducts();

        public Product GetProductById(int id);

        public Product UpdateProduct(int id, string newName, string newCategory, int newPrice);

        public Product DeleteProduct(int id);
    }
}
