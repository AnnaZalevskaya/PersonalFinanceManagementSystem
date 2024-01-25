using Accounts.BusinessLogic.Models;
using Operations.Application.Settings;

namespace Operations.Application.MassTransit.Requests
{
    public class GetOperationsByAccountRequest
    {
        public FinancialAccountModel Account { get; set; }
        public PaginationSettings PaginationSettings { get; set; }
        public int Id { get; set; } 
    }
}
