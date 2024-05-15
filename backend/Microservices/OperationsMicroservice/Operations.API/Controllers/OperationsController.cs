using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operations.Application.Models;
using Operations.Application.Models.Consts;
using Operations.Application.Operations.Commands.CreateOperation;
using Operations.Application.Operations.Commands.DeleteAccountOperations;
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
        [Authorize(Policy = AuthPolicyConsts.AdminOnly)]
        public async Task<ActionResult> GetAllAsync([FromQuery] PaginationSettings paginationSettings)
        {
            var operations = await _mediator.Send(new GetOperationListQuery(paginationSettings));

            return Ok(operations);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OperationModel>> GetByIdAsync(string id)
        {
            var operation = await _mediator.Send(new GetOperationDetailsQuery(id));

            return Ok(operation);
        }

        [HttpGet("account/{accountId}")]
        [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<ActionResult<List<OperationModel>>> GetByAccountIdAsync(int accountId,
            [FromQuery] PaginationSettings paginationSettings)
        {
            var operations = await _mediator
                .Send(new GetOperationListByAccountIdQuery(accountId, paginationSettings));

            return Ok(operations);
        }

        [HttpPost]
        [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateOperationModel model)
        {
            await _mediator.Send(new CreateOperationCommand(model));

            return NoContent();
        }

        [HttpDelete("account/{accountId}")]
        [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<ActionResult> DeleteByAccountIdAsync(int accountId)
        {
            await _mediator.Send(new DeleteAccountOperationsCommand(accountId));

            return NoContent();
        }
    }
}
