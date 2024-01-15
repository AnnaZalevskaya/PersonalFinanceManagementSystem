using Abp.Domain.Entities;
using AutoMapper;
using MediatR;
using Operations.Application.Interfaces;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.GetCategoryDetails
{
    public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery, CategoryModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryModel> Handle(GetCategoryDetailsQuery query,
            CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.GetAsync(query.Id, cancellationToken);

            if (category == null)
            {
                throw new EntityNotFoundException("Category not found");
            }

            return _mapper.Map<CategoryModel>(category);
        }
    }
}
