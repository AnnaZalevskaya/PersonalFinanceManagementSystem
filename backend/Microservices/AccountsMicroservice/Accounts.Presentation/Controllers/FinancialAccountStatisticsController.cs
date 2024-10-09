using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Presentation.Controllers
{
    [ApiController]
    [Route("api/financial-account-statistics")]
    public class FinancialAccountStatisticsController : ControllerBase 
    {
        private readonly IAccountStatisticsService _service;

        public FinancialAccountStatisticsController(IAccountStatisticsService service)
        {
            _service = service;
        }

        [HttpGet("type/{accountTypeParam}")]
        public async Task<ActionResult<AccountStatisticsModel>> GetAccountStatisticsAsync(int accountTypeParam)
        {
            var statistics = await _service.GetStatisticByAccountsAsync(accountTypeParam);

            return Ok(statistics);
        }
    }
}
