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
        private readonly ICacheRepository _cacheRepository;

        public GetOperationListByAccountIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, 
            IMessageConsumer consumer, ICacheRepository cacheRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _consumer = consumer;
            _cacheRepository = cacheRepository;
        }

        public async Task<List<OperationModel>> Handle(GetOperationListByAccountIdQuery query,
            CancellationToken cancellationToken)
        {
            int id = _consumer.ConsumeMessage(query.AccountId);

            if (id == 0)
            {
                throw new Exception("The user's account was not found");
            }

            var entities = await _unitOfWork.Operations
                .GetByAccountIdAsync(query.AccountId, query.paginationSettings, cancellationToken);
            var operationsList = _mapper.Map<List<OperationModel>>(entities);
            var cachedData = new List<OperationModel>();

            foreach (var operation in operationsList)
            {
                cachedData.Add(await _cacheRepository.GetDataCacheAsync<OperationModel>(operation.Id));
            }

            if (cachedData.Count == 0)
            {
                throw new Exception("There is no relevant information in the cache");
            }

            return operationsList;
        }
    }
}
