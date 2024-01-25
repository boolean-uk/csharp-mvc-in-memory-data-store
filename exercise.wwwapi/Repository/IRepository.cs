namespace exercise.wwwapi.Repository
{
    public interface IRepository<T>
    {
        T Add(T info);
        T Delete(int id);
         T GetById(int id);
        T Update(T newInfo, int id );
        List<T> GetAll();
        List<T> Get(string inputCategory);


    }
}
