using MediatR;
using Operations.Application.Interfaces;

namespace Operations.Application.Operations.Queries.RecordsCount.GetCategoryRecordsCount
{
    public class GetCategoryRecordsCountQueryHandler : IRequestHandler<GetCategoryRecordsCountQuery, long>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryRecordsCountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(GetCategoryRecordsCountQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Categories.GetRecordsCountAsync();
        }
    }
}
