namespace wwwapi.Repository
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T Add(T val);
        bool Delete(int id);

        T? Get(int id);

        T Update(T val);
    }
}
