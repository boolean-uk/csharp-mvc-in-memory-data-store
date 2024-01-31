namespace exercise.wwwapi.Repository
{
    public interface IRepo<T>
    {
        T Add (T item);
        IEnumerable<T> GetAll();
        T GetSingle(Func<T, bool> predicate);
        T Update (T item);
        T Delete(Func<T, bool> predicate);

    }
}
