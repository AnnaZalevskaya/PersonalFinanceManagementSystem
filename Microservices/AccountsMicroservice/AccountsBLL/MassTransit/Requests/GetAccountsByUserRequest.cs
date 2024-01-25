using Accounts.DataAccess.Settings;

namespace Accounts.BusinessLogic.MassTransit.Requests
{
    public class GetAccountsByUserRequest
    {
        public int UserId { get; set; }
        public PaginationSettings PaginationSettings { get; set; }
    }
}
