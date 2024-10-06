using MediatR;
using Operations.Application.Interfaces;

namespace Operations.Application.Operations.Queries.RecordsCount.GetAccountOperationRecordsCount
{
    public class GetAccountOperationRecordsCountQueryHandler 
        : IRequestHandler<GetAccountOperationRecordsCountQuery, long>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountOperationRecordsCountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(GetAccountOperationRecordsCountQuery query, 
            CancellationToken cancellationToken)
        {
            return await _unitOfWork.Operations.GetAccountRecordsCountAsync(query.AccountId);
        }
    }
}
