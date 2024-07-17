using Operations.Application.Settings;
using Operations.Core.Entities;

namespace Operations.Application.Interfaces
{
    public interface IRecurringPaymentRepository
    {
        Task<string> CreateAsync(RecurringPayment payment, CancellationToken cancellationToken);
        Task DeleteByAccountIdAsync(int accountId, CancellationToken cancellationToken);
        Task DeleteByIdAsync(string id, CancellationToken cancellationToken);
        Task<RecurringPayment> UpdateAsync(string id, RecurringPayment model, CancellationToken cancellationToken);
        Task<RecurringPayment> GetAsync(string id, CancellationToken cancellationToken);
        Task<IEnumerable<RecurringPayment>> GetAllAsync(PaginationSettings paginationSettings, 
            CancellationToken cancellationToken);
        Task<IEnumerable<RecurringPayment>> GetByUserIdAsync(int accountId, PaginationSettings paginationSettings, 
            CancellationToken cancellationToken);
        Task<IEnumerable<RecurringPayment>> GetByAccountIdAsync(int accountId, PaginationSettings paginationSettings, 
            CancellationToken cancellationToken);
        Task<long> GetUserRecordsCountAsync(int userId);
    }
}
