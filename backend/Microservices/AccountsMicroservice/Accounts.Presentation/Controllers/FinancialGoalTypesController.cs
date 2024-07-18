using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Presentation.Controllers
{
    [ApiController]
    [Route("api/financial-goal-types")]
    public class FinancialGoalTypesController : ControllerBase
    {
        private readonly IFinancialGoalTypeService _service;

        public FinancialGoalTypesController(IFinancialGoalTypeService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialGoalTypeModel>> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            return Ok(await _service.GetByIdAsync(id, cancellationToken));
        }

        [HttpGet]
        public async Task<ActionResult<List<FinancialGoalTypeModel>>> GetAllAsync([FromQuery] PaginationSettings paginationSettings,
            CancellationToken cancellationToken)
        {
            return Ok(await _service.GetAllAsync(paginationSettings, cancellationToken));
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetRecordsCountAsync()
        {
            return Ok(await _service.GetRecordsCountAsync());
        }
    }
}
