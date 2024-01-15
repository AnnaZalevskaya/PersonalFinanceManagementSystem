using MediatR;
using Operations.Application.Models;
using Operations.Application.Settings;

namespace Operations.Application.Operations.Queries.GetOperationList
{
    public class GetOperationListQuery : IRequest<List<OperationModel>>
    {
        public PaginationSettings paginationSettings;

        public GetOperationListQuery(PaginationSettings settings)
        {
            paginationSettings = settings;
        }
    }
}
