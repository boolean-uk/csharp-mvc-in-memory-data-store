using genericapi.api.Models;

namespace genericapi.api.Repository
{
    public interface IRepository<T, U> where T : IModel<U>
    {
        Task<T> Get(U id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Update(T entity);
        Task<bool> Delete(U id);
        Task<T> Add(T entity);
    }
}
