namespace exercise.wwwapi.Models
{
    public class ErrorTypes
    {
        public string message {  get; set; }
        int id { get; set; }

        public ErrorTypes(string Message, int id)
        {
            message = Message;
            this.id = id;
        }

        public int getId()
        {
            return id;
        }
    }
}
