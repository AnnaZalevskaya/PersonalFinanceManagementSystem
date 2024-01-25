using Accounts.BusinessLogic.MassTransit.Requests;
using Accounts.BusinessLogic.MassTransit.Responses;
using Accounts.BusinessLogic.Models;
using Accounts.DataAccess.Settings;
using Auth.Application.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Presentation.Controllers
{
    [ApiController]
    [Route("api/financial-accounts")]
    public class FinancialAccountsController : ControllerBase
    {
        private readonly IBusControl _busControl;
        private readonly Uri _rabbitMqUrl = new Uri("rabbitmq://localhost/accountsQueue");

        public FinancialAccountsController(IBusControl busControl)
        {
            _busControl = busControl;
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewAccountAsync([FromBody] FinancialAccountModel model)
        {
            var user = HttpContext.Items["User"] as UserModel;
            var productsResponse = await GetResponseRabbitTask<CreateAccountsRequest, 
                ICollection<FinancialAccountModel>>(new CreateAccountsRequest()
            {
                AccountId = model.Id,
                User = user
            });

            return Ok(productsResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> CloseAccountAsync(int id, CancellationToken cancellationToken)
        {
            var user = HttpContext.Items["User"] as UserModel;
            await GetResponseRabbitTask<DeleteAccountRequest,
                GetAccountsResponse>(new DeleteAccountRequest()
                {
                    Id = id,
                    User = user
                });

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAccountAsync(int id, [FromBody] FinancialAccountModel model)
        {
            var user = HttpContext.Items["User"] as UserModel;
            await GetResponseRabbitTask<UpdateAccountRequest,
                GetAccountsResponse>(new UpdateAccountRequest()
                {
                    Id = id,
                    Model = model,
                    User = user
                });

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialAccountModel>> GetAccountAsync(int id, 
            CancellationToken cancellationToken) 
        {
            var user = HttpContext.Items["User"] as UserModel;
            var response = await GetResponseRabbitTask<GetAccountRequest, 
                FinancialAccountModel>(new GetAccountRequest
            {
                Id = id,
                User = user
            });

            return Ok(response);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<FinancialAccountModel>>> GetUserAccountsAsync(int userId,
            [FromQuery] PaginationSettings paginationSettings)
        {
            var response = await GetResponseRabbitTask<GetAccountsByUserRequest, 
                ICollection<FinancialAccountModel>>(new GetAccountsByUserRequest
            {
                UserId = userId,
                PaginationSettings = paginationSettings
            });

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<FinancialAccountModel>>> GetAllAccountsAsync([FromQuery] PaginationSettings paginationSettings)
        {
            var user = HttpContext.Items["User"] as UserModel;
            var response = await GetResponseRabbitTask<UserModel, ICollection<FinancialAccountModel>>(user);

            return Ok(response);
        }

        private async Task<TOut> GetResponseRabbitTask<TIn, TOut>(TIn request)
            where TIn : class
            where TOut : class
        {
            var client = _busControl.CreateRequestClient<TIn>(_rabbitMqUrl);
            var response = await client.GetResponse<TOut>(request);
            return response.Message;
        }
    }
}
