using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        public List<Product> getAll();
        public List<Product> getAll(string category);

        public Product Add(Product product);

        public Product getAProduct(int Id);

        public Product updateProduct(int Id, Product product, out string errorReason);

        public Product deleteAProduct(int Id);
    }
}