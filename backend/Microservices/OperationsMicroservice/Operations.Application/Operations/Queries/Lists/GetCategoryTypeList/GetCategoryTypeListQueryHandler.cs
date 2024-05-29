using AutoMapper;
using MediatR;
using Operations.Application.Interfaces;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.Lists.GetCategoryTypeList
{
    public class GetCategoryTypeListQueryHandler : IRequestHandler<GetCategoryTypeListQuery,
        List<CategoryTypeModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryTypeListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryTypeModel>> Handle(GetCategoryTypeListQuery query,
            CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.CategoryTypes
                .GetAllAsync(query.paginationSettings, cancellationToken);

            return _mapper.Map<List<CategoryTypeModel>>(entities);
        }
    }
}