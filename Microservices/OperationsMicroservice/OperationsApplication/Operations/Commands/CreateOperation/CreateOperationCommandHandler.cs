using AutoMapper;
using MediatR;
using Operations.Application.Consumers;
using Operations.Application.Interfaces;
using Operations.Core.Entities;

namespace Operations.Application.Operations.Commands.CreateOperation
{
    public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMessageConsumer _consumer;
        private readonly ICacheRepository _cacheRepository;

        public CreateOperationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMessageConsumer consumer, 
            ICacheRepository cacheRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _consumer = consumer;
            _cacheRepository = cacheRepository;
        }

        public async Task Handle(CreateOperationCommand command, CancellationToken cancellationToken)
        {
            int userId = _consumer.ConsumeMessage(command.Model.AccountId);

            if (userId == 0)
            {
                throw new Exception("The user's account was not found");
            }

            var entity = _mapper.Map<Operation>(command.Model);

            await _unitOfWork.Operations.CreateAsync(entity, cancellationToken);

            await _cacheRepository.CacheDataAsync(entity.Id, command.Model);
        }
    }
}
