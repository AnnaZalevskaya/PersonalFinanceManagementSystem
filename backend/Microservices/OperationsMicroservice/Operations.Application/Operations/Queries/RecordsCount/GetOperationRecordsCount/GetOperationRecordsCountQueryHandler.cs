using MediatR;
using Operations.Application.Interfaces;

namespace Operations.Application.Operations.Queries.RecordsCount.GetOperationRecordsCount
{
    public class GetOperationRecordsCountQueryHandler : IRequestHandler<GetOperationRecordsCountQuery, long>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOperationRecordsCountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(GetOperationRecordsCountQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Operations.GetRecordsCountAsync();
        }
    }
}
