using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Models.Consts;
using Accounts.BusinessLogic.Models.Enums;
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
        private readonly IFinancialGoalService _service;

        public FinancialGoalsController(IFinancialGoalService service)
        {
            _service = service;
        }

        [HttpGet]
      //  [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<ActionResult<List<FinancialGoalModel>>> GetGoalsAsync([FromQuery] PaginationSettings paginationSettings, 
            CancellationToken cancellationToken)
        {
            return Ok(await _service.GetFinancialGoalsAsync(paginationSettings, cancellationToken));
        }

        [HttpGet("account/{accountId}")]
      //  [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<ActionResult<List<FinancialGoalModel>>> GetAccountGoalsAsync(int accountId, 
            [FromQuery] PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            return Ok(await _service.GetAccountFinancialGoalsAsync(accountId, paginationSettings, 
                cancellationToken));
        }

        [HttpGet("count_for_account/{accountId}")]
       // [Authorize]
        public async Task<ActionResult<int>> GetUserRecordsCountAsync(int accountId)
        {
            return Ok(await _service.GetAccountRecordsCountAsync(accountId));
        }

        [HttpPost]
       // [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<IActionResult> CreateGoalAsync([FromBody] FinancialGoalActionModel financialGoal, 
            CancellationToken cancellationToken)
        {
            await _service.CreateFinancialGoalAsync(financialGoal, cancellationToken);

            return NoContent();
        }

        [HttpGet("{id}")]
       // [Authorize]
        public async Task<ActionResult<FinancialAccountModel>> GetAccountGoalByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            return Ok(await _service.GetByIdAsync(id, cancellationToken));
        }

        [HttpDelete("account/{accountId}/goal/{id}")]
       // [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<IActionResult> CloseAccountAsync(int accountId, int id,
            CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(id, cancellationToken);

            return NoContent();
        }

        [HttpPut("account/{accountId}/goal/{id}")]
        // [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<IActionResult> EditAccountGoalAsync(int accountId, int id,
            [FromBody] FinancialGoalActionModel model, CancellationToken cancellationToken)
        {
            await _service.UpdateAsync(id, model, cancellationToken);

            return NoContent();
        }

        [HttpPatch("goal/{goalId}")]
        public async Task<IActionResult> GetByIdAsync(int goalId, GoalStatusEnum goalStatus)
        {
            await _service.UpdateGoalStatusAsync(goalId, goalStatus);

            return NoContent();
        }
    }
}
