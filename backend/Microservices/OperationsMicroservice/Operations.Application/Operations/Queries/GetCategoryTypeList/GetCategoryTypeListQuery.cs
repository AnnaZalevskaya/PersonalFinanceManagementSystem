using MediatR;
using Operations.Application.Models;
using Operations.Application.Settings;

namespace Operations.Application.Operations.Queries.GetCategoryTypeList
{
    public class GetCategoryTypeListQuery : IRequest<List<CategoryTypeModel>>
    {
        public PaginationSettings paginationSettings;

        public GetCategoryTypeListQuery(PaginationSettings settings)
        {
            paginationSettings = settings;
        }
    }
}
