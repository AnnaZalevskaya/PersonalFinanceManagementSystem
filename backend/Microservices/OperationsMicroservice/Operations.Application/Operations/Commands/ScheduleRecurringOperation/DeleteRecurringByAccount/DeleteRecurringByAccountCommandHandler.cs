using Hangfire;
using MediatR;
using Operations.Application.Interfaces;

namespace Operations.Application.Operations.Commands.ScheduleRecurringOperation.DeleteRecurringByAccount
{
    public class DeleteRecurringByAccountCommandHandler : IRequestHandler<DeleteRecurringByAccountCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRecurringByAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteRecurringByAccountCommand command, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.RecurringPayments
                .GetByAccountIdAsync(command.AccountId, command.PaginationSettings, cancellationToken);

            if(entities != null)
            {
                await _unitOfWork.RecurringPayments.DeleteByAccountIdAsync(command.AccountId, cancellationToken);

                foreach(var entity in entities) 
                {
                    RecurringJob.RemoveIfExists(entity.Id);
                }                
            }            
        }
    }
}
