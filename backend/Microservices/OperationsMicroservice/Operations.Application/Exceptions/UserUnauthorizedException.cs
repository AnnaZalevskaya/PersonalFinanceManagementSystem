namespace Operations.Application.Exceptions
{
    public class UserUnauthorizedException : Exception
    {
        public UserUnauthorizedException() { }

        public UserUnauthorizedException(string message) : base(message) { }
    }
}
