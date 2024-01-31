using AutoMapper;
using MediatR;
using Operations.Application.Consumers;
using Operations.Application.Interfaces;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.GetOperationList
{
    public class GetOperationListByAccountIdQueryHandler : IRequestHandler<GetOperationListByAccountIdQuery, 
        List<OperationModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMessageConsumer _consumer;

        public GetOperationListByAccountIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, 
            IMessageConsumer consumer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _consumer = consumer;
        }

        public async Task<List<OperationModel>> Handle(GetOperationListByAccountIdQuery query,
            CancellationToken cancellationToken)
        {
            int id = _consumer.ConsumeMessage(query.AccountId);

            if (id == 0)
            {
                throw new Exception("The account was not found");
            }

            var entities = await _unitOfWork.Operations
                .GetByAccountIdAsync(query.AccountId, query.paginationSettings, cancellationToken);

            return _mapper.Map<List<OperationModel>>(entities);
        }
    }
}
