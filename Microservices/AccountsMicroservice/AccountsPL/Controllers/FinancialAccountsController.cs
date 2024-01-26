using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Settings;
using Auth.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Presentation.Controllers
{
    [ApiController]
    [Route("api/financial-accounts")]
    public class FinancialAccountsController : ControllerBase
    {
        private readonly IAccountMessageService _messageService;

        public FinancialAccountsController(IAccountMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewAccountAsync([FromBody] FinancialAccountModel model)
        {
            var user = HttpContext.Items["User"] as UserModel;
            var account = await _messageService.CreateNewAccountAsync(model, user);

            return Ok(account);
        }

        [HttpDelete]
        public async Task<IActionResult> CloseAccountAsync(int id)
        {
            var user = HttpContext.Items["User"] as UserModel;
            await _messageService.CloseAccountAsync(id, user);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAccountAsync(int id, [FromBody] FinancialAccountModel model)
        {
            var user = HttpContext.Items["User"] as UserModel;
            await _messageService.EditAccountAsync(id, model, user);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialAccountModel>> GetAccountAsync(int id) 
        {
            var user = HttpContext.Items["User"] as UserModel;
            var account = await _messageService.GetAccountAsync(id, user);

            return Ok(account);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<FinancialAccountModel>>> GetUserAccountsAsync(int userId,
            [FromQuery] PaginationSettings paginationSettings)
        {
            var accounts = await _messageService.GetUserAccountsAsync(userId, paginationSettings);

            return Ok(accounts);
        }

        [HttpGet]
        public async Task<ActionResult<List<FinancialAccountModel>>> GetAllAccountsAsync([FromQuery] PaginationSettings paginationSettings)
        {
            var response = await _messageService.GetAllAccountsAsync(paginationSettings);

            return Ok(response);
        }
    }
}
