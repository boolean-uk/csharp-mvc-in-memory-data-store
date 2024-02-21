using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllAsync();
        public Task<Product?> GetByIdAsync(int id);
        public Task<Product> AddAsync(ProductCreatePayload payload);
        public Task<Product?> UpdateAsync(int id, ProductUpdatePayload productData);
        public Task<bool> DeleteAsync(int id);

    }
}
