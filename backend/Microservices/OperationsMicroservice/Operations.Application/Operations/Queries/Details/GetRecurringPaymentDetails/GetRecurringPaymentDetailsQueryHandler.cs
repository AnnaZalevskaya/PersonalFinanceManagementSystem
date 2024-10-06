using AutoMapper;
using MediatR;
using Operations.Application.Interfaces;
using Operations.Application.Models;
using Operations.Core.Exceptions;

namespace Operations.Application.Operations.Queries.Details.GetRecurringPaymentDetails
{
    public class GetRecurringPaymentDetailsQueryHandler 
        : IRequestHandler<GetRecurringPaymentDetailsQuery, RecurringPaymentModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRecurringPaymentDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RecurringPaymentModel> Handle(GetRecurringPaymentDetailsQuery query,
            CancellationToken cancellationToken)
        {
            var operation = await _unitOfWork.RecurringPayments.GetAsync(query.Id, cancellationToken);

            if (operation == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<RecurringPaymentModel>(operation);
        }
    }
}
