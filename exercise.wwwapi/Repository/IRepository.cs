using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository<M>
    {
        public M Add(M model);
        public List<M> GetAll();
        public List<M> GetAll(string category);
        public M Get(int id);
        public M Update(int id, M model);
        public M Delete(int id);
    }
}
