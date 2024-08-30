namespace exercise.wwwapi.Model
{
    public class ErrorMessage
    {
        public string message { get; set; }

        public ErrorMessage(string message)
        {
            this.message = message;
        }
    }
}
