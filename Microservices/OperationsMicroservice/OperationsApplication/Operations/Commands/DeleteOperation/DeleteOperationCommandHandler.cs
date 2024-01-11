using Abp.Domain.Entities;
using MediatR;
using Operations.Application.Interfaces;

namespace Operations.Application.Operations.Commands.DeleteOperation
{
    public class DeleteOperationCommandHandler : IRequestHandler<DeleteOperationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOperationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteOperationCommand command, CancellationToken cancellationToken)
        {
            var operation = await _unitOfWork.Operations.GetAsync(command.Id, cancellationToken);
            if (operation == null)
            {
                throw new EntityNotFoundException("Operation not found");
            }

            await _unitOfWork.Operations.DeleteAsync(command.Id, cancellationToken);
        }
    }
}
