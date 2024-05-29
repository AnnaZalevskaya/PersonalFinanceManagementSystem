using AutoMapper;
using MediatR;
using Operations.Application.Interfaces;
using Operations.Application.Models;
using Operations.Core.Exceptions;

namespace Operations.Application.Operations.Queries.Details.GetCategoryTypeDetails
{
    public class GetCategoryTypeDetailsQueryHandler
        : IRequestHandler<GetCategoryTypeDetailsQuery, CategoryTypeModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryTypeDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryTypeModel> Handle(GetCategoryTypeDetailsQuery query,
            CancellationToken cancellationToken)
        {
            var type = await _unitOfWork.CategoryTypes.GetAsync(query.Id, cancellationToken);

            if (type == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<CategoryTypeModel>(type);
        }
    }
}
