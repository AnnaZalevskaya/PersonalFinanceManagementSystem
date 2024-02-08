using AutoMapper;
using MediatR;
using Operations.Application.Consumers;
using Operations.Application.Interfaces;
using Operations.Application.Interfaces.gRPC;
using Operations.Application.Models;
using Operations.Application.Operations.Commands.gRPC;

namespace Operations.Application.Operations.Queries.GetOperationList
{
    public class GetOperationListByAccountIdQueryHandler : IRequestHandler<GetOperationListByAccountIdQuery, 
        List<OperationModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMessageConsumer _consumer;
        private readonly IOperationsGrpcRepository _operationsGrpcRepository;

        public GetOperationListByAccountIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, 
            IMessageConsumer consumer, IOperationsGrpcRepository operationsGrpcRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _consumer = consumer;
            _operationsGrpcRepository = operationsGrpcRepository;
        }

        public async Task<List<OperationModel>> Handle(GetOperationListByAccountIdQuery query,
            CancellationToken cancellationToken)
        {
            // _handler.TestMethod(query.AccountId);
            var operations = await _operationsGrpcRepository.GetByAccountIdAsync(query.AccountId);
            int id = _consumer.ConsumeMessage(query.AccountId);

            if (id == 0)
            {
                throw new Exception("The user's account was not found");
            }

            var entities = await _unitOfWork.Operations
                .GetByAccountIdAsync(query.AccountId, query.paginationSettings, cancellationToken);

            return _mapper.Map<List<OperationModel>>(entities);
        }
    }
}
