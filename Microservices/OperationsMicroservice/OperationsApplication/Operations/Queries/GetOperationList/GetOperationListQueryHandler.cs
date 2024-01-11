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

        public GetOperationListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OperationModel>> Handle(GetOperationListQuery query, 
            CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.Operations
                .GetAllAsync(query.paginationSettings, cancellationToken);

            return _mapper.Map<List<OperationModel>>(entities);
        }
    }
}
