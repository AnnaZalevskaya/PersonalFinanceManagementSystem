using Abp.Domain.Entities;
using MediatR;
using Operations.Application.Consumers;
using Operations.Application.Interfaces;

namespace Operations.Application.Operations.Commands.DeleteOperation
{
    public class DeleteOperationCommandHandler : IRequestHandler<DeleteOperationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageConsumer _consumer;
        private readonly ICacheRepository _cacheRepository;

        public DeleteOperationCommandHandler(IUnitOfWork unitOfWork, IMessageConsumer consumer, ICacheRepository cacheRepository)
        {
            _unitOfWork = unitOfWork;
            _consumer = consumer;
            _cacheRepository = cacheRepository;
        }

        public async Task Handle(DeleteOperationCommand command, CancellationToken cancellationToken)
        {
            var operation = await _unitOfWork.Operations.GetAsync(command.Id, cancellationToken);

            if (operation == null)
            {
                throw new EntityNotFoundException("Operation not found");
            }

            int receivedAccountId = _consumer.ConsumeMessage(operation.AccountId);

            if (receivedAccountId == 0 || command.AccountId != receivedAccountId)
            {
                throw new Exception("The user's account was not found");
            }

            await _unitOfWork.Operations.DeleteAsync(command.Id, cancellationToken);

            await _cacheRepository.RemoveCachedDataAsync(command.Id);
        }
    }
}
