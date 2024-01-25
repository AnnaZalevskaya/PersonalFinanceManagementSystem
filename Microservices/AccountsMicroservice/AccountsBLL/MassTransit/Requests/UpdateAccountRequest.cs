using Accounts.BusinessLogic.Models;
using Auth.Application.Models;

namespace Accounts.BusinessLogic.MassTransit.Requests
{
    public class UpdateAccountRequest
    {
        public int Id { get; set; }
        public FinancialAccountModel Model { get; set; }
        public UserModel User {  get; set; }
    }
}
