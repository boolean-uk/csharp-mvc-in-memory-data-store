using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetAll();
        public Product? GetById(int id);
        public Product Add(Product product);
        public Product? Update(int id, ProductUpdatePayload productData);
        public bool Delete(int id);

    }
}
