using MongoDB.Driver;
using Operations.Application.Interfaces;
using Operations.Application.Settings;
using Operations.Core.Entities;
using Operations.Infrastructure.Data;

namespace Operations.Infrastructure.Repositories
{
    public class RecurringPaymentRepository : IRecurringPaymentRepository
    {
        private readonly OperationsDbContext _context;

        public RecurringPaymentRepository(OperationsDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateAsync(RecurringPayment payment, CancellationToken cancellationToken)
        {
            await _context.RecurringPayments.InsertOneAsync(payment, cancellationToken);

            return payment.Id;
        }

        public async Task DeleteByAccountIdAsync(int accountId, CancellationToken cancellationToken)
        {
            await _context.RecurringPayments
                .DeleteManyAsync(payment => payment.AccountId == accountId, cancellationToken);
        }

        public async Task DeleteByIdAsync(string id, CancellationToken cancellationToken)
        {
            await _context.RecurringPayments
                .FindOneAndDeleteAsync(payment => payment.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<RecurringPayment> UpdateAsync(string id, RecurringPayment model, 
            CancellationToken cancellationToken)
        {
            return await _context.RecurringPayments
                .FindOneAndReplaceAsync(payment => payment.Id == id, model, cancellationToken: cancellationToken);
        }

        public async Task<RecurringPayment> GetAsync(string id, CancellationToken cancellationToken)
        {
            var payment = await _context.RecurringPayments
                .Find(operation => operation.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

            return payment;
        }

        public async Task<IEnumerable<RecurringPayment>> GetAllAsync(PaginationSettings paginationSettings,
            CancellationToken cancellationToken)
        {
            var payments = await _context.RecurringPayments
                .Find(operations => true)
                .SortBy(operation => operation.AccountId)
                .ThenByDescending(operation => operation.StartDate)
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Limit(paginationSettings.PageSize)
                .ToListAsync(cancellationToken);

            return payments;
        }

        public async Task<IEnumerable<RecurringPayment>> GetByUserIdAsync(int userId,
            PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var payments = await _context.RecurringPayments
                .Find(operation => operation.UserId == userId)
                .SortBy(operation => operation.AccountId)
                .ThenByDescending(operation => operation.StartDate)
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Limit(paginationSettings.PageSize)
                .ToListAsync(cancellationToken);

            return payments;
        }

        public async Task<IEnumerable<RecurringPayment>> GetByAccountIdAsync(int accountId,
            PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var payments = await _context.RecurringPayments
                .Find(operation => operation.AccountId == accountId)
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Limit(paginationSettings.PageSize)
                .ToListAsync(cancellationToken);

            return payments;
        }

        public async Task<long> GetUserRecordsCountAsync(int userId)
        {
            return await _context.RecurringPayments
                .Find(operation => operation.UserId == userId)
                .CountDocumentsAsync();
        }
    }
}
