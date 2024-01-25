using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository.Interfaces
{
    public interface IRepository
    {
        public ProductItem Add(ProductPayload payLoad);
        public ProductItem Get(int id);
        public List<ProductItem> GetAll();
        public List<ProductItem> GetAll(string category);
        public ProductItem Update(int id, ProductPayload updatePayload);
        public ProductItem Delete(int id);
    }
}
