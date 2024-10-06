using AutoMapper;
using MediatR;
using Operations.Application.Interfaces;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.Lists.GetUserRecurringPaymentList
{
    public class GetUserRecPaymentListQueryHandler
        : IRequestHandler<GetUserRecPaymentListQuery, List<RecurringPaymentModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserRecPaymentListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<RecurringPaymentModel>> Handle(GetUserRecPaymentListQuery query, 
            CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.RecurringPayments
                .GetByUserIdAsync(query.UserId, query.paginationSettings, cancellationToken);
            var operationsList = _mapper.Map<List<RecurringPaymentModel>>(entities);

            return operationsList;
        }
    }
}
