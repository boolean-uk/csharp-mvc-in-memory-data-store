namespace exercise.wwwapi.View
{
    public interface IProduct<T> where T : class
    {
        List<T> GetAll(string category);
        T CreateProduct(T entity);
        T Get(int id);
        T Update(T entity, int id);
        T Delete(int id);

    }
}
