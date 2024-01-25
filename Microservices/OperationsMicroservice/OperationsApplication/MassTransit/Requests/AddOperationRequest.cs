using Accounts.BusinessLogic.Models;
using Operations.Application.Models;

namespace Operations.Application.MassTransit.Requests
{
    public class AddOperationRequest
    {
        public FinancialAccountModel Account { get; set; }
        public CreateOperationModel Operation { get; set; }
    }
}
