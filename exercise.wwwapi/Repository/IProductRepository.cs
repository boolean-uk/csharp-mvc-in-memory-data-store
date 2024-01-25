using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetAll();
        public Product? GetProductByID(int id);
        public Product Delete(int id);

        public Product Create(ProductPayload data);
        public Product? Update(int id, ProductPayload data);
    }
}
