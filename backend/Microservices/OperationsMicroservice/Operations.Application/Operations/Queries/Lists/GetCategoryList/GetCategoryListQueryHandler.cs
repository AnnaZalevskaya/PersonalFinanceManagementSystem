using AutoMapper;
using MediatR;
using Operations.Application.Interfaces;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.Lists.GetCategoryList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, List<CategoryModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryModel>> Handle(GetCategoryListQuery query,
            CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.Categories.GetAllAsync(query.paginationSettings, cancellationToken);

            return _mapper.Map<List<CategoryModel>>(entities);
        }
    }
}
