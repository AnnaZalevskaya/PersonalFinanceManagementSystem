using Accounts.BusinessLogic.Models;
using Auth.Application.Models;

namespace Accounts.BusinessLogic.MassTransit.Requests
{
    public class CreateAccountsRequest
    {
        public UserModel User { get; set; }
        public int AccountId { get; set; }
        public List<FinancialAccountModel> Accounts { get; set; }
    }
}
