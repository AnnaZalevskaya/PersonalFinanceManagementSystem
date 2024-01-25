using Accounts.BusinessLogic.Models;
using Auth.Application.Models;

namespace Operations.Application.MassTransit.Requests
{
    public class GetOperationRequest
    {
        public FinancialAccountModel Account { get; set; }
        public string Id { get; set; }
    }
}
