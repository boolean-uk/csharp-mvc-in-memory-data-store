using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetAll();
        public Product? GetProduct(int id);
        public Product AddProduct(string name, string category, int price);
        public Product? UpdateProduct(int id, ProductUpdatePayload updateData);
        public Product? RemoveProduct(int id);
    }
}
