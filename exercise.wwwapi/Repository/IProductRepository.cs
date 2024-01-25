using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        public Product CreateAProduct(ProductPostPayload productPost);

        public List<Product> GetAllProducts();

        public Product? GetAProduct(int id);

        public Product? UpdateAProduct(int id, ProductUpdateData updateData);

        public bool DeleteAProduct(int id);
    }
}
