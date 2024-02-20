using MediatR;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.GetCategoryDetails
{
    public class GetCategoryDetailsQuery : IRequest<CategoryModel>
    {
        public int Id { get; set; }

        public GetCategoryDetailsQuery(int id)
        {
            Id = id;
        }
    }
}
