using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repositories
{
    public interface IRepository
    {
        public List<Product> GetProducts(string category);
        public Product GetSingleProduct(int id);
        public Product AddProduct(Product product);
        public Product RemoveProduct(Product product);
        public Product UpdateProduct(int id, Product product);
        public bool ProductExists(string productName);
    }
}
