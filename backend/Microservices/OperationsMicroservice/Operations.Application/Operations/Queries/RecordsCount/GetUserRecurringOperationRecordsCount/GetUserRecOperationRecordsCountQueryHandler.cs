using MediatR;
using Operations.Application.Interfaces;

namespace Operations.Application.Operations.Queries.RecordsCount.GetUserRecurringOperationRecordsCount
{
    public class GetUserRecOperationRecordsCountQueryHandler : IRequestHandler<GetUserRecOperationRecordsCountQuery, long>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserRecOperationRecordsCountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(GetUserRecOperationRecordsCountQuery query,
            CancellationToken cancellationToken)
        {
            return await _unitOfWork.RecurringPayments.GetUserRecordsCountAsync(query.UserId);
        }
    }
}
