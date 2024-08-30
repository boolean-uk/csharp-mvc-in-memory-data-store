namespace exercise.wwwapi.Exceptions
{
    public class PriceException: Exception
    {
        public PriceException() : base()
        {

        }
        public PriceException(string message) : base(message)
        {
        }

        public PriceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
