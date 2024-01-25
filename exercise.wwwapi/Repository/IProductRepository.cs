using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IProductRepository
    {
        public IEnumerable<InternalProduct> Get();
        public InternalProduct Get(int id);
        public InternalProduct Create(Product product);
        public InternalProduct Update(int id, Product product);
        public InternalProduct Delete(int id);
    }
}
