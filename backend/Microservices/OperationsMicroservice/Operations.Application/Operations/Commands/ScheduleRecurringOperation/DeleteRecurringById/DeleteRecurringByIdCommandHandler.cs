using Hangfire;
using MediatR;
using Operations.Application.Interfaces;

namespace Operations.Application.Operations.Commands.ScheduleRecurringOperation.DeleteRecurringById
{
    public class DeleteRecurringByIdCommandHandler : IRequestHandler<DeleteRecurringByIdCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRecurringByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteRecurringByIdCommand command, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.RecurringPayments.GetAsync(command.Id, cancellationToken);

            if (entity != null)
            {
                await _unitOfWork.RecurringPayments.DeleteByIdAsync(command.Id, cancellationToken);
                RecurringJob.RemoveIfExists(entity.Id);
            }
        }
    }
}
