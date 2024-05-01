using MediatR;
using Operations.Application.Models;
using Operations.Application.Settings;

namespace Operations.Application.Operations.Queries.GetCategoryList
{
    public class GetCategoryListQuery : IRequest<List<CategoryModel>>
    {
        public PaginationSettings paginationSettings;

        public GetCategoryListQuery(PaginationSettings settings)
        {
            paginationSettings = settings;
        }
    }
}
