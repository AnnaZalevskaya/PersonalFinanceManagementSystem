using MediatR;
using Operations.Application.Settings;

namespace Operations.Application.Operations.Commands.ScheduleRecurringOperation.DeleteRecurringByAccount
{
    public class DeleteRecurringByAccountCommand : IRequest
    {
        public int AccountId { get; set; }
        public PaginationSettings PaginationSettings {  get; set; } 

        public DeleteRecurringByAccountCommand(int accountId)
        {
            AccountId = accountId;
            PaginationSettings = new PaginationSettings()
            {
                PageNumber = 1,
                PageSize = 1
            };
        }
    }
}
