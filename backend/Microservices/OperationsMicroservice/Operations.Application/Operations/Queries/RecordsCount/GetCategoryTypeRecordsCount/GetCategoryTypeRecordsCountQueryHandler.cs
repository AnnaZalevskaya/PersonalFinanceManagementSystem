using MediatR;
using Operations.Application.Interfaces;

namespace Operations.Application.Operations.Queries.RecordsCount.GetCategoryTypeRecordsCount
{
    public class GetCategoryTypeRecordsCountQueryHandler 
        : IRequestHandler<GetCategoryTypeRecordsCountQuery, long>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryTypeRecordsCountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(GetCategoryTypeRecordsCountQuery query, 
            CancellationToken cancellationToken)
        {
            return await _unitOfWork.CategoryTypes.GetRecordsCountAsync();
        }
    }
}
