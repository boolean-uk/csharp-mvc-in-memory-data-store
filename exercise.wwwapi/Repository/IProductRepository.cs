using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAll();
        public Task<Product?> GetById(int id);
        public Task<Product> Add(ProductCreatePayload payload);
        public Task<Product?> Update(int id, ProductUpdatePayload productData);
        public Task<bool> Delete(int id);

    }
}
