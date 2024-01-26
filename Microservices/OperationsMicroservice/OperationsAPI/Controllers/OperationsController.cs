using Accounts.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Operations.Application.Interfaces;
using Operations.Application.Models;
using Operations.Application.Settings;

namespace Operations.API.Controllers
{
    [ApiController]
    [Route("api/operations")]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationMessageService _messageService;

        public OperationsController(IOperationMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync([FromQuery] PaginationSettings paginationSettings)
        {
            var operations = await _messageService.GetAllAsync(paginationSettings);

            return Ok(operations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationModel>> GetByIdAsync(string id)
        {
            var account = HttpContext.Items["Account"] as FinancialAccountModel;
            var operation = _messageService.GetByIdAsync(id, account);

            return Ok(operation);
        }

        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<List<OperationModel>>> GetByAccountIdAsync(int accountId,
            [FromQuery] PaginationSettings paginationSettings)
        {
            var account = HttpContext.Items["Account"] as FinancialAccountModel;
            var operations = _messageService.GetByAccountIdAsync(accountId, paginationSettings, account);

            return Ok(operations);
        }

        [HttpPost]
        public async Task<ActionResult<OperationModel>> CreateAsync([FromBody] CreateOperationModel model)
        {
            var account = HttpContext.Items["Account"] as FinancialAccountModel;
            var newOperation = await _messageService.CreateAsync(model, account);

            return Ok(newOperation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFromHistoryAsync(string id)
        {
            var account = HttpContext.Items["Account"] as FinancialAccountModel;
            await _messageService.DeleteFromHistoryAsync(id, account);

            return NoContent();
        }
    }
}
