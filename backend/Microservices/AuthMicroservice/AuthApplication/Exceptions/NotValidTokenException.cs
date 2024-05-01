namespace Auth.Application.Exceptions
{
    public class NotValidTokenException : Exception
    {
        public NotValidTokenException() { }

        public NotValidTokenException(string message) : base(message) { }
    }
}