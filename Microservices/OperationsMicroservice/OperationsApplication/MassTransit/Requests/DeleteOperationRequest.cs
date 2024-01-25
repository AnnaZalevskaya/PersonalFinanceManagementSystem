using Accounts.BusinessLogic.Models;

namespace Operations.Application.MassTransit.Requests
{
    public class DeleteOperationRequest
    {
        public FinancialAccountModel Account { get; set; }
        public string Id { get; set; }
    }
}
