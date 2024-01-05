using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Presentation.Controllers
{
    [ApiController]
    [Route("api/financial-account-types")]
    public class FinancialAccountTypesController : ControllerBase
    {
        private readonly IFinancialAccountTypeService _service;

        public FinancialAccountTypesController(IFinancialAccountTypeService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialAccountTypeModel>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return Ok(await _service.GetByIdAsync(id, cancellationToken));
        }

        [HttpGet]
        public async Task<ActionResult<List<FinancialAccountTypeModel>>> GetAllAsync([FromQuery] PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            return Ok(await _service.GetAllAsync(paginationSettings, cancellationToken));
        }
    }
}
