using Accounts.BusinessLogic.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Operations.Application.MassTransit.Requests;
using Operations.Application.MassTransit.Responses;
using Operations.Application.Models;
using Operations.Application.Settings;

namespace Operations.API.Controllers
{
    [ApiController]
    [Route("api/operations")]
    public class OperationsController : ControllerBase
    {
        private readonly IBusControl _busControl;
        private readonly Uri _rabbitMqUrl = new Uri("rabbitmq://localhost/operationsQueue");

        public OperationsController(IBusControl busControl)
        {
            _busControl = busControl;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync([FromQuery] PaginationSettings paginationSettings)
        {
            var account = HttpContext.Items["Account"] as FinancialAccountModel;
            var response = await GetResponseRabbitTask<FinancialAccountModel, 
                ICollection<OperationModel>>(account);
            
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationModel>> GetByIdAsync(string id)
        {
            var account = HttpContext.Items["Account"] as FinancialAccountModel;
            var response = await GetResponseRabbitTask<GetOperationRequest, 
                OperationModel>(new GetOperationRequest
                {
                    Id = id,
                    Account = account
                });

            return Ok(response);
        }

        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<List<OperationModel>>> GetByAccountIdAsync(int accountId,
            [FromQuery] PaginationSettings paginationSettings)
        {
            var account = HttpContext.Items["Account"] as FinancialAccountModel;
            var response = await GetResponseRabbitTask<GetOperationsByAccountRequest,
                OperationModel>(new GetOperationsByAccountRequest
                {
                    Id = accountId,
                    Account = account,
                    PaginationSettings = paginationSettings,
                });

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CreateOperationModel model)
        {
            var account = HttpContext.Items["Account"] as FinancialAccountModel;
            await GetResponseRabbitTask<AddOperationRequest, GetOperationsResponse>(new AddOperationRequest()
            {
                Account = account,
                Operation = model
            });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFromHistoryAsync(string id)
        {
            var account = HttpContext.Items["Account"] as FinancialAccountModel;
            await GetResponseRabbitTask<DeleteOperationRequest, GetOperationsResponse>(new DeleteOperationRequest()
            {
                Account = account,
                Id = id
            });

            return NoContent();
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
