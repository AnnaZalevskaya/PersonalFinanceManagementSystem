using AutoMapper;
using MediatR;
using Operations.Application.Interfaces;
using Operations.Core.Entities;

namespace Operations.Application.Operations.Commands.CreateOperation
{
    public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOperationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(CreateOperationCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Operation>(command);

            await _unitOfWork.Operations.CreateAsync(entity, cancellationToken);
        }
    }
}
