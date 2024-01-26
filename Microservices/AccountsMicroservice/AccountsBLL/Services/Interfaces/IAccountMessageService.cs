using Accounts.BusinessLogic.MassTransit.Responses;
using Accounts.BusinessLogic.Models;
using Accounts.DataAccess.Settings;
using Auth.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.BusinessLogic.Services.Interfaces
{
    public interface IAccountMessageService
    {
        public Task<ActionResult<GetAccountsResponse>> CreateNewAccountAsync(FinancialAccountModel model,
            UserModel user);
        public Task<IActionResult> CloseAccountAsync(int id, UserModel user);
        public Task<IActionResult> EditAccountAsync(int id, FinancialAccountModel model, UserModel user);
        public Task<ActionResult<GetAccountResponse>> GetAccountAsync(int id, UserModel user);
        public Task<ActionResult<GetAccountsResponse>> GetUserAccountsAsync(int userId, 
            PaginationSettings paginationSettings);
        public Task<ActionResult<GetAccountsResponse>> GetAllAccountsAsync(PaginationSettings paginationSettings);
    }
}
