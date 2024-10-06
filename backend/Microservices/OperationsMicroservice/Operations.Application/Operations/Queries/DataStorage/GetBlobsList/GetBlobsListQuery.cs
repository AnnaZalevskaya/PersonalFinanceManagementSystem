using MediatR;

namespace Operations.Application.Operations.Queries.DataStorage.GetBlobsList
{
    public class GetBlobsListQuery : IRequest<IEnumerable<string>>
    {
    }
}
