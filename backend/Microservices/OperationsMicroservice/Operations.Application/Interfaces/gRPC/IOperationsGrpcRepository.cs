using Operations.Core.Entities;

namespace Operations.Application.Interfaces.gRPC
{
    public interface IOperationsGrpcRepository
    {
        Task<IEnumerable<AggregationResult>> GetByAccountIdAsync(int accountId, CancellationToken cancellationToken);
        Task<IEnumerable<AggregationResult>> GetByAccountIdByPeriodAsync(int accountId, string start_date, string end_date, 
            CancellationToken cancellationToken);
    }
}
