using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetByCategory(string input);

        public Product GetById(int id);
        public Product CreateProduct(ProductPost model);

        public Product UpdateProduct(int id, ProductPost model);

        public Product DeleteProduct(int id);

    }
}
