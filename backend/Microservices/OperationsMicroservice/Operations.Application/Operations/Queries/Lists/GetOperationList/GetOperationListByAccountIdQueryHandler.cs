using AutoMapper;
using MediatR;
using Operations.Application.Consumers;
using Operations.Application.Exceptions;
using Operations.Application.Interfaces;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.Lists.GetOperationList
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

            //if (id == 0)
            //{
            //    throw new UserUnauthorizedException();
            //}

            var cachedOperations = await _cacheRepository
                .GetCachedLargeDataAsync<OperationModel>(query.paginationSettings, query.AccountId.ToString());

            if (cachedOperations.Count != 0)
            {
                return cachedOperations;
            }

            var entities = await _unitOfWork.Operations
                .GetByAccountIdAsync(query.AccountId, query.paginationSettings, cancellationToken);
            var operationsList = _mapper.Map<List<OperationModel>>(entities);

            await _cacheRepository.CacheLargeDataAsync(query.paginationSettings, operationsList, query.AccountId.ToString());

            return operationsList;
        }
    }
}
