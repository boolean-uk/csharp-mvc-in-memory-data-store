namespace wwwapi.Repository
{
    public interface IRepository<T1, T2>
    {
        Task<List<T1>> GetAll();
        Task<T1> Add(T1 val);
        Task<bool> Delete(int id);

        Task<T1?> Get(int id);

        Task<T1> Update(int id, T2 updatePayload);
    }
}
