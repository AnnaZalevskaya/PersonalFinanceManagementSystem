using Accounts.BusinessLogic.Models;

namespace Accounts.BusinessLogic.MassTransit.Responses
{
    public class GetAccountsResponse
    {
        public string UserName { get; set; } 
        public List<FinancialAccountModel> Accounts { get; set; }
    }
}
