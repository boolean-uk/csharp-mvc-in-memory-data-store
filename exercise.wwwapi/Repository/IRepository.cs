using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        public IEnumerable<Product> GetProducts();

        public IEnumerable<Product> GetProducts(string? category);

        public Product AddProduct(UserProduct product);

        public Product GetProduct(int id);

        public Product UpdateProduct(Product product, UserProduct userProduct);

        public Product RemoveProduct(Product product);
    }
}
