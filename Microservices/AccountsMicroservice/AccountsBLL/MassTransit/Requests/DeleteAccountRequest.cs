using Auth.Application.Models;

namespace Accounts.BusinessLogic.MassTransit.Requests
{
    public class DeleteAccountRequest
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
    }
}
