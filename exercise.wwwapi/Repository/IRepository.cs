using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        public IEnumerable<Product> GetAllProducts(string category);

        public Product AddProduct(InPuProduct product);

        public Product GetAProduct(int id);

        public Product UpdateAProduct(int id, InPuProduct product);

        public Product DeleteABook(int id);
    }
}
