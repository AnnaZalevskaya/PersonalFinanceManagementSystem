using MediatR;
using Operations.Application.Models;
using Operations.Application.Operations.Commands.CreateOperation;

namespace Operations.Application.Operations.Commands.ScheduleRecurringOperation
{
    public class PaymentProcessor
    {
        private readonly IMediator _mediator;

        public PaymentProcessor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ProcessPayment(RecurringPaymentModel payment, CancellationToken cancellationToken)
        {
            var model = new CreateOperationModel()
            {
                AccountId = payment.AccountId,
                Description = new Dictionary<string, object>()
                {
                    { "Amount", payment.Amount },
                    { "Name", payment.Name }
                }
            };

            await _mediator.Send(new CreateOperationCommand(model));
        }
    }
}
