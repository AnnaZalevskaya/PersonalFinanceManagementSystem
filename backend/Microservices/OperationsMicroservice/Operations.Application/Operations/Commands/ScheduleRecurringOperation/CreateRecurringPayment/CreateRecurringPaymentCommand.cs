using MediatR;
using Operations.Application.Models;

namespace Operations.Application.Operations.Commands.ScheduleRecurringOperation.CreateRecurringPayment
{
    public class CreateRecurringPaymentCommand : IRequest<string>
    {
        public RecurringPaymentActionModel Model { get; set; }

        public CreateRecurringPaymentCommand(RecurringPaymentActionModel model)
        {
            Model = model;
        }
    }
}
