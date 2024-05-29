using AutoMapper;
using MediatR;
using Operations.Application.Interfaces;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.Lists.GetOperationList
{
    public class GetOperationListQueryHandler : IRequestHandler<GetOperationListQuery, List<OperationModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheRepository _cacheRepository;

        public GetOperationListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheRepository cacheRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheRepository = cacheRepository;
        }

        public async Task<List<OperationModel>> Handle(GetOperationListQuery query,
            CancellationToken cancellationToken)
        {
            var cachedOperations = await _cacheRepository
                .GetCachedLargeDataAsync<OperationModel>(query.paginationSettings);

            if (cachedOperations.Count != 0)
            {
                return cachedOperations;
            }

            var entities = await _unitOfWork.Operations
                .GetAllAsync(query.paginationSettings, cancellationToken);
            var operationsList = _mapper.Map<List<OperationModel>>(entities);
            var cachedData = new List<OperationModel>();

            if (cachedData.Count == operationsList.Count)
            {
                return cachedData;
            }

            await _cacheRepository.CacheLargeDataAsync(query.paginationSettings, operationsList);

            return operationsList;
        }
    }
}
