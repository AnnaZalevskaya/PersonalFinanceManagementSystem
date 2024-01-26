using Accounts.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Operations.Application.MassTransit.Responses;
using Operations.Application.Models;
using Operations.Application.Settings;

namespace Operations.Application.Interfaces
{
    public interface IOperationMessageService
    {
        public Task<ActionResult<GetOperationsResponse>> GetAllAsync(PaginationSettings paginationSettings);
        public Task<ActionResult<OperationModel>> GetByIdAsync(string id, FinancialAccountModel account);
        public Task<ActionResult<GetOperationsResponse>> GetByAccountIdAsync(int accountId,
            PaginationSettings paginationSettings, FinancialAccountModel account);
        public Task<ActionResult<OperationModel>> CreateAsync(CreateOperationModel model,
            FinancialAccountModel account);
        public Task<IActionResult> DeleteFromHistoryAsync(string id, FinancialAccountModel account);
    }
}
