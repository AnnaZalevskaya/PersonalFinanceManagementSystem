using Abp.Domain.Entities;
using AutoMapper;
using MediatR;
using Operations.Application.Interfaces;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.GetOperationDetails
{
    public class GetOperationDetailsQueryHandler : IRequestHandler<GetOperationDetailsQuery, OperationModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOperationDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationModel> Handle(GetOperationDetailsQuery query, 
            CancellationToken cancellationToken)
        {
            var operation = await _unitOfWork.Operations.GetAsync(query.Id, cancellationToken);

            if (operation == null)
            {
                throw new EntityNotFoundException("Operation not found");
            }

            return _mapper.Map<OperationModel>(operation);
        }
    }
}
