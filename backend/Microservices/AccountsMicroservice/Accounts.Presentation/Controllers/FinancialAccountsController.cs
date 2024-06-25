using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Models.Consts;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Presentation.Controllers
{
    [ApiController]
    [Route("api/financial-accounts")]
    public class FinancialAccountsController : ControllerBase
    {
        private readonly IFinancialAccountService _service;

        public FinancialAccountsController(IFinancialAccountService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<ActionResult> CreateNewAccountAsync([FromBody] FinancialAccountActionModel model,
            CancellationToken cancellationToken)
        {
            await _service.AddAsync(model, cancellationToken);

            return NoContent();
        }

        [HttpDelete("user/{userId}/account/{accountId}")]
        [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<IActionResult> CloseAccountAsync(int userId, int accountId,
            CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(userId, accountId, cancellationToken);

            return NoContent();
        }

        [HttpPut("user/{userId}/account/{accountId}")]
        [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<IActionResult> EditAccountAsync(int userId, int accountId,
            [FromBody] FinancialAccountActionModel model, CancellationToken cancellationToken)
        {
            await _service.UpdateAsync(userId, accountId, model, cancellationToken);

            return NoContent();
        }

        [HttpGet("{id}")]
    //    [Authorize]
        public async Task<ActionResult<FinancialAccountModel>> GetAccountByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            return Ok(await _service.GetByIdAsync(id, cancellationToken));
        }

        [HttpGet("user/{userId}")]
        [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<ActionResult<List<FinancialAccountModel>>> GetUserAccountsAsync(int userId,
            [FromQuery] PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            return Ok(await _service.GetAccountsByUserIdAsync(userId, paginationSettings, cancellationToken));
        }

        [HttpGet]
        [Authorize(Policy = AuthPolicyConsts.AdminOnly)]
        public async Task<ActionResult<List<FinancialAccountModel>>> GetAllAccountsAsync([FromQuery] PaginationSettings paginationSettings,
            CancellationToken cancellationToken)
        {
            return Ok(await _service.GetAllAsync(paginationSettings, cancellationToken));
        }

        [HttpGet("count")]
        [Authorize]
        public async Task<ActionResult<int>> GetRecordsCountAsync()
        {
            return Ok(await _service.GetRecordsCountAsync());
        }

        [HttpGet("count_for_user/{userId}")]
        [Authorize]
        public async Task<ActionResult<int>> GetUserRecordsCountAsync(int userId)
        {
            return Ok(await _service.GetUserRecordsCountAsync(userId));
        }
    }
}