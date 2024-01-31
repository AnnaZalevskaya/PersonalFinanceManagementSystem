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

        public CreateOperationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMessageConsumer consumer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _consumer = consumer;
        }

        public async Task Handle(CreateOperationCommand command, CancellationToken cancellationToken)
        {
            int id = _consumer.ConsumeMessage(command.Model.AccountId);

            if (id == 0)
            {
                throw new Exception("The account was not found");
            }

            var entity = _mapper.Map<Operation>(command.Model);

            await _unitOfWork.Operations.CreateAsync(entity, cancellationToken);
        }
    }
}
