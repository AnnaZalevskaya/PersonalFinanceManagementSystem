namespace Auth.Application.Exceptions
{
    public class BadCredentialsException : Exception
    {
        public BadCredentialsException() { }

        public BadCredentialsException(string message) : base(message) { }
    }
}