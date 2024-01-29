using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAll();
        public Task<Product?> GetProductByID(int id);
        public Task<Product> Delete(int id);

        public Task<Product> Create(ProductPayload data);
        public Task<Product?> Update(int id, ProductPayload data);
    }
}
