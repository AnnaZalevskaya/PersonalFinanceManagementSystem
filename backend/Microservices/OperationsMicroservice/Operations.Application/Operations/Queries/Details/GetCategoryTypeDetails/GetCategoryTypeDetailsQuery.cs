using MediatR;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.Details.GetCategoryTypeDetails
{
    public class GetCategoryTypeDetailsQuery : IRequest<CategoryTypeModel>
    {
        public int Id { get; set; }

        public GetCategoryTypeDetailsQuery(int id)
        {
            Id = id;
        }
    }
}
