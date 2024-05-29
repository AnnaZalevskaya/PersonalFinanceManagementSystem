using MediatR;
using Operations.Application.Models;
using Operations.Application.Settings;

namespace Operations.Application.Operations.Queries.Lists.GetOperationList
{
    public class GetOperationListByAccountIdQuery : IRequest<List<OperationModel>>
    {
        public int AccountId { get; set; }
        public PaginationSettings paginationSettings;

        public GetOperationListByAccountIdQuery(int id, PaginationSettings settings)
        {
            AccountId = id;
            paginationSettings = settings;
        }
    }
}
