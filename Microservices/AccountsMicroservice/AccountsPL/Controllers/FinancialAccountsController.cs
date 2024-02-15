using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Settings;
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
        public async Task<ActionResult> CreateNewAccountAsync([FromBody] FinancialAccountActionModel model,
            CancellationToken cancellationToken)
        {
            await _service.AddAsync(model, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{userId}/{accountId}")]
        public async Task<IActionResult> CloseAccountAsync(int userId, int accountId, 
            CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(userId, accountId, cancellationToken);

            return NoContent();
        }

        [HttpPut("{accountId}")]
        public async Task<IActionResult> EditAccountAsync(int userId, int accountId, 
            [FromBody] FinancialAccountActionModel model, CancellationToken cancellationToken)
        {
            await _service.UpdateAsync(userId, accountId, model, cancellationToken);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialAccountModel>> GetAccountAsync(int id, 
            CancellationToken cancellationToken)
        {
            return Ok(await _service.GetByIdAsync(id, cancellationToken));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<FinancialAccountModel>>> GetUserAccountsAsync(int userId,
            [FromQuery] PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            return Ok(await _service.GetAccountsByUserIdAsync(userId, paginationSettings, cancellationToken));
        }

        [HttpGet]
        public async Task<ActionResult<List<FinancialAccountModel>>> GetAllAccountsAsync([FromQuery] PaginationSettings paginationSettings,
            CancellationToken cancellationToken)
        {
            return Ok(await _service.GetAllAsync(paginationSettings, cancellationToken));
        }
    }
}