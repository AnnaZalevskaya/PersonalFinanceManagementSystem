using Auth.Application.Models;

namespace Accounts.BusinessLogic.MassTransit.Requests
{
    public class GetAccountRequest
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
    }
}
