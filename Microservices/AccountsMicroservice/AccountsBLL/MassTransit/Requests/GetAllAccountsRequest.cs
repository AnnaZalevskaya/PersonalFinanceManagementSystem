using Accounts.DataAccess.Settings;
using Auth.Application.Models;

namespace Accounts.BusinessLogic.MassTransit.Requests
{
    public class GetAllAccountsRequest
    {
        public UserModel User { get; set; }
        public PaginationSettings PaginationSettings { get; set; }
    }
}
