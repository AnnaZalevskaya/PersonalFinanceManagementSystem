using MediatR;

namespace Operations.Application.Operations.Commands.ScheduleRecurringOperation.AddOrUpdateRecurring
{
    public class RecurringCommand : IRequest
    {
        public string Id { get; set; }

        public RecurringCommand(string id)
        {
            Id = id;
        }
    }
}
