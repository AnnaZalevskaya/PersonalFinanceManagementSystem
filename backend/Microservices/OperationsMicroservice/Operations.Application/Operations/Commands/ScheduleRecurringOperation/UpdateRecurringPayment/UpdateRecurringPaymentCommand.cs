using MediatR;
using Operations.Application.Models;

namespace Operations.Application.Operations.Commands.ScheduleRecurringOperation.UpdateRecurringPayment
{
    public class UpdateRecurringPaymentCommand : IRequest<RecurringPaymentModel>
    {
        public string PaymentId { get; set; }
        public RecurringPaymentActionModel UpdatedPayment { get; set; }

        public UpdateRecurringPaymentCommand(string id, RecurringPaymentActionModel model)
        {
            PaymentId = id;
            UpdatedPayment = model;
        }
    }
}
