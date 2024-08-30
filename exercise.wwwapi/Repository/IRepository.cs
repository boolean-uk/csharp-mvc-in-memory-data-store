using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository<T> where T : class
    {
        public T AddProduct(T item);
        public List<T> GetAllProducts(string category);
        public T GetProduct(int id);
        public T UpdateProduct(int id, T item);
        public T DeleteProduct(int id);
    }
}
