using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Models.Consts;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Presentation.Controllers
{
    [ApiController]
    [Route("api/financial-goals")]
    public class FinancialGoalsController : ControllerBase
    {
        private readonly IFinancialGoalService _financialGoalsService;

        public FinancialGoalsController(IFinancialGoalService financialGoalsService)
        {
            _financialGoalsService = financialGoalsService;
        }

        [HttpGet]
      //  [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<ActionResult<List<FinancialGoalModel>>> GetGoalsAsync([FromQuery] PaginationSettings paginationSettings, 
            CancellationToken cancellationToken)
        {
            return Ok(await _financialGoalsService.GetFinancialGoalsAsync(paginationSettings, cancellationToken));
        }

        [HttpGet("account/{accountId}")]
      //  [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<ActionResult<List<FinancialGoalModel>>> GetAccountGoalsAsync(int accountId, 
            [FromQuery] PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            return Ok(await _financialGoalsService.GetAccountFinancialGoalsAsync(accountId, paginationSettings, 
                cancellationToken));
        }

        [HttpGet("count_for_account/{accountId}")]
       // [Authorize]
        public async Task<ActionResult<int>> GetUserRecordsCountAsync(int accountId)
        {
            return Ok(await _financialGoalsService.GetAccountRecordsCountAsync(accountId));
        }

        [HttpPost]
       // [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<IActionResult> CreateGoalAsync([FromBody] FinancialGoalActionModel financialGoal, 
            CancellationToken cancellationToken)
        {
            await _financialGoalsService.CreateFinancialGoalAsync(financialGoal, cancellationToken);

            return NoContent();
        }

        [HttpPut("account/{accountId}/goal/{id}")]
       // [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<IActionResult> EditAccountGoalAsync(int accountId, int id,
            [FromBody] FinancialGoalActionModel model, CancellationToken cancellationToken)
        {
            await _financialGoalsService.UpdateAsync(id, model, cancellationToken);

            return NoContent();
        }

        [HttpGet("{id}")]
       // [Authorize]
        public async Task<ActionResult<FinancialAccountModel>> GetAccountGoalByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            return Ok(await _financialGoalsService.GetByIdAsync(id, cancellationToken));
        }

        [HttpDelete("account/{accountId}/goal/{id}")]
       // [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<IActionResult> CloseAccountAsync(int accountId, int id,
            CancellationToken cancellationToken)
        {
            await _financialGoalsService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
