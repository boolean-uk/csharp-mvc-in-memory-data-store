namespace exercise.wwwapi.Repositories
{
    public interface IRepository<T1, T2> where T1 : class where T2 : class
    {
        bool Exists(T2 entity);
        T1 Create(T2 entity);

        List<T1> GetAll(string category);

        T1? Get(int identifier);

        T1? Update(T2 entity, int identifier);

        T1? Delete(int identifier);
    }
}
