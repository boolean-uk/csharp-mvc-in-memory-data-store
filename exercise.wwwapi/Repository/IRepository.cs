
namespace exercise.wwwapi.Repository
{
    public interface IRepository<T> where T : class
    {
        public T CreateProduct(T product);

        public T UpdateProduct(int id, T product);

        public T GetProductById(int id);

        //public List<T> GetAll();

        public T DeleteProduct(int id);
        public List<T> GetAll(string? category);
    }
}
