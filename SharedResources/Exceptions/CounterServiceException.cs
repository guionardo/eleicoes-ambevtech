namespace SharedResources.Exceptions
{
    public class CounterServiceException : Exception
    {
        public CounterServiceException(string message) : base(message) { }

        public CounterServiceException() : base()
        {
        }

        public CounterServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
