namespace Accounts.BusinessLogic.Exceptions
{
    public class GetBalanceException : Exception
    {
        public GetBalanceException() { }

        public GetBalanceException(string message) : base(message) { }
    }
}