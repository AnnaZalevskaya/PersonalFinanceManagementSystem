using Operations.Core.Entities;

namespace Operations.Application.Interfaces.gRPC
{
    public interface IOperationsGrpcRepository
    {
        Task<IEnumerable<AggregationResult>> GetByAccountIdAsync(int accountId, CancellationToken cancellationToken);
    }
}
