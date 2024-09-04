namespace exercise.wwwapi.ViewModels
{
    public class Payload<T> where T : class
    {
        public DateTime date { get; set; }
        public T data { get; set; }
    }
}
