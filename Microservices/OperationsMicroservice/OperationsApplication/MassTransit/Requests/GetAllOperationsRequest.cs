using Accounts.BusinessLogic.Models;
using Operations.Application.Models;
using Operations.Application.Settings;

namespace Operations.Application.MassTransit.Requests
{
    public class GetAllOperationsRequest
    {
        public FinancialAccountModel Account { get; set; }
        public PaginationSettings PaginationSettings { get; set; }
    }
}
