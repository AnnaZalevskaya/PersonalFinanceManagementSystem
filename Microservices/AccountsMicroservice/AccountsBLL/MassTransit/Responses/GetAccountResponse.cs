using Accounts.BusinessLogic.Models;

namespace Accounts.BusinessLogic.MassTransit.Responses
{
    public class GetAccountResponse
    {
        public string UserName { get; set; }
        public FinancialAccountModel Account { get; set; }
    }
}
