using exercise.wwwapi.Models.Data;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        public Product AddProduct(Product product);

        public Product UpdateProduct(int id, Product product);

        public Product GetProduct(int id);

        public Product DeleteProduct(int id);

        public Product GetAll();
    }
}
