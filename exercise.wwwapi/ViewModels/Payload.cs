namespace exercise.wwwapi.ViewModels
{
    public class Payload<T> where T : class
    {
        public T data { get; set; }
    }
}
