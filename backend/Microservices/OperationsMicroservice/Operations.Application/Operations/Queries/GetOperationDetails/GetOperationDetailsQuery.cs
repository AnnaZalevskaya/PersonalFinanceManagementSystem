using MediatR;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.GetOperationDetails
{
    public class GetOperationDetailsQuery : IRequest<OperationModel>
    {
        public string Id { get; set; }

        public GetOperationDetailsQuery(string id)
        {
             Id = id;
        }
    }
}
