using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operations.Application.Models;
using Operations.Application.Operations.Commands.DeleteAccountOperations;
using Operations.Application.Operations.Commands.ScheduleRecurringOperation.AddOrUpdateRecurring;
using Operations.Application.Operations.Commands.ScheduleRecurringOperation.CreateRecurringPayment;
using Operations.Application.Operations.Commands.ScheduleRecurringOperation.DeleteRecurringById;
using Operations.Application.Operations.Commands.ScheduleRecurringOperation.UpdateRecurringPayment;
using Operations.Application.Operations.Queries.Details.GetOperationDetails;
using Operations.Application.Operations.Queries.Details.GetRecurringPaymentDetails;
using Operations.Application.Operations.Queries.Lists.GetUserRecurringPaymentList;
using Operations.Application.Operations.Queries.RecordsCount.GetUserRecurringOperationRecordsCount;
using Operations.Application.Settings;

namespace Operations.API.Controllers
{
    [ApiController]
    [Route("api/schedule-recurring-operations")]
    public class ScheduleRecurringOperationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScheduleRecurringOperationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecurringPayment([FromBody] RecurringPaymentActionModel model)
        {
            var id = await _mediator.Send(new CreateRecurringPaymentCommand(model));
            await _mediator.Send(new RecurringCommand(id));

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyRecurringPayment(string id, [FromBody] RecurringPaymentActionModel model)
        {
            await _mediator.Send(new UpdateRecurringPaymentCommand(id, model));

            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> RemoveRecurringPayment(string id)
        {
            await _mediator.Send(new DeleteRecurringByIdCommand(id));

            return NoContent();
        }

        [HttpDelete("account/{accountId}")]
        public async Task<IActionResult> RemoveAccountRecurringPayment(int accountId)
        {
            await _mediator.Send(new DeleteAccountOperationsCommand(accountId));

            return NoContent();
        }

        [HttpGet("{id}")]
      //  [Authorize]
        public async Task<ActionResult<RecurringPaymentModel>> GetByIdAsync(string id)
        {
            var operation = await _mediator.Send(new GetRecurringPaymentDetailsQuery(id));

            return Ok(operation);
        }

        [HttpGet("user/{userId}")]
       // [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<ActionResult<List<OperationModel>>> GetByUserIdAsync(int userId,
            [FromQuery] PaginationSettings paginationSettings)
        {
            var recurringOperations = await _mediator
                .Send(new GetUserRecPaymentListQuery(userId, paginationSettings));

            return Ok(recurringOperations);
        }

        [HttpGet("count_for_user/{userId}")]
        public async Task<ActionResult<long>> GetAccountRecordsCountAsync(int userId)
        {
            return Ok(await _mediator.Send(new GetUserRecOperationRecordsCountQuery(userId)));
        }
    }
}
