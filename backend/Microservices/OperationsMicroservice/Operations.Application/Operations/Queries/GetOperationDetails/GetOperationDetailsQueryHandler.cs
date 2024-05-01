using AutoMapper;
using MediatR;
using Operations.Application.Interfaces;
using Operations.Application.Models;
using Operations.Core.Exceptions;

namespace Operations.Application.Operations.Queries.GetOperationDetails
{
    public class GetOperationDetailsQueryHandler : IRequestHandler<GetOperationDetailsQuery, OperationModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheRepository _cacheRepository;

        public GetOperationDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheRepository cacheRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheRepository = cacheRepository;
        }

        public async Task<OperationModel> Handle(GetOperationDetailsQuery query, 
            CancellationToken cancellationToken)
        {
            var cachedObj = await _cacheRepository.GetCachedDataAsync<OperationModel>(query.Id);

            if (cachedObj != null)
            {
                return cachedObj;
            }

            var operation = await _unitOfWork.Operations.GetAsync(query.Id, cancellationToken);

            if (operation == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<OperationModel>(operation);
        }
    }
}
