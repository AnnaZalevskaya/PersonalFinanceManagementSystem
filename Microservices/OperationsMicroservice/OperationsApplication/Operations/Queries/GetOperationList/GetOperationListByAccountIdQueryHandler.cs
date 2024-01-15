using AutoMapper;
using MediatR;
using Operations.Application.Interfaces;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.GetOperationList
{
    public class GetOperationListByAccountIdQueryHandler : IRequestHandler<GetOperationListByAccountIdQuery, 
        List<OperationModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOperationListByAccountIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OperationModel>> Handle(GetOperationListByAccountIdQuery query,
            CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.Operations
                .GetByAccountIdAsync(query.AccountId, query.paginationSettings, cancellationToken);

            return _mapper.Map<List<OperationModel>>(entities);
        }
    }
}
