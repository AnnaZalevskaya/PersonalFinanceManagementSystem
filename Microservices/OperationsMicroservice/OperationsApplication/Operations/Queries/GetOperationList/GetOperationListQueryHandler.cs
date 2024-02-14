using AutoMapper;
using MediatR;
using Operations.Application.Interfaces;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.GetOperationList
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
            var entities = await _unitOfWork.Operations
                .GetAllAsync(query.paginationSettings, cancellationToken);
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
