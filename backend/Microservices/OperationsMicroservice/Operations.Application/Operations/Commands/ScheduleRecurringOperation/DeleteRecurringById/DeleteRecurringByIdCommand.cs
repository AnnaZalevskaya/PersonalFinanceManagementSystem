using MediatR;

namespace Operations.Application.Operations.Commands.ScheduleRecurringOperation.DeleteRecurringById
{
    public class DeleteRecurringByIdCommand : IRequest
    {
        public string Id { get; set; }

        public DeleteRecurringByIdCommand(string id)
        {
            Id = id;
        }
    }
}
