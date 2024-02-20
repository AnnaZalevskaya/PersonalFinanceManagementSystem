using MediatR;
using Microsoft.AspNetCore.Mvc;
using Operations.Application.Models;
using Operations.Application.Operations.Commands.CreateOperation;
using Operations.Application.Operations.Commands.DeleteOperation;
using Operations.Application.Operations.Queries.GetOperationDetails;
using Operations.Application.Operations.Queries.GetOperationList;
using Operations.Application.Settings;

namespace Operations.API.Controllers
{
    [ApiController]
    [Route("api/operations")]
    public class OperationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OperationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync([FromQuery] PaginationSettings paginationSettings)
        {
            var operations = await _mediator.Send(new GetOperationListQuery(paginationSettings));

            return Ok(operations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationModel>> GetByIdAsync(string id)
        {
            var operation = await _mediator.Send(new GetOperationDetailsQuery(id));

            return Ok(operation);
        }

        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<List<OperationModel>>> GetByAccountIdAsync(int accountId,
            [FromQuery] PaginationSettings paginationSettings)
        {
            var operations = await _mediator
                .Send(new GetOperationListByAccountIdQuery(accountId, paginationSettings));

            return Ok(operations);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CreateOperationModel model)
        {
            await _mediator.Send(new CreateOperationCommand(model));

            return NoContent();
        }

        [HttpDelete("{accountId}/{operationId}")]
        public async Task<IActionResult> DeleteFromHistoryAsync(int accountId, string operationId)
        {
            await _mediator.Send(new DeleteOperationCommand(accountId, operationId));

            return NoContent();
        }
    }
}
