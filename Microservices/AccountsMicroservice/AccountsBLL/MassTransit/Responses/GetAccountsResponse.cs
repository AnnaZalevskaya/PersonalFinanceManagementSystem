using Accounts.BusinessLogic.Models;

namespace Accounts.BusinessLogic.MassTransit.Responses
{
    public class GetAccountsResponse
    {
        public int UserId { get; set; } 
        public List<FinancialAccountModel> Accounts { get; set; }
    }
}
