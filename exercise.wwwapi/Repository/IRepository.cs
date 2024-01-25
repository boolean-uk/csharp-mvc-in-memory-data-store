using exercise.wwwapi.Objects;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        public IEnumerable<Product> GetProducts(string category);
        public Product GetProductById(int id);
        public Product CreateProduct(InputProduct NewProduct);
        public Product UpdateProduct(InputProduct UpdatedProduct, int id);
        public Product DeleteProduct(int id);
    }
}
