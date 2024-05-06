using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        public Products AddProduct(string name, string category, int price);
        public List<Products> GetAllProducts();
        public bool ProductExists(string name);
        public Products? GetProductById(int id);
        public Products? UpdateProduct(int id, ProductUpdatePayload newUpdateData);
        public bool DeleteProduct(int id);
    }
}
