using Operations.Application.Interfaces;
using Operations.Infrastructure.Data;
using Operations.Core.Exceptions;

namespace Operations.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OperationsDbContext _context;

        public UnitOfWork(OperationsDbContext context)
        {
            _context = context ?? throw new DatabaseNotFoundException();
        }

        public ICategoryRepository Categories
        {
            get
            {
                return new CategoryRepository(_context);
            }
        }

        public ICategoryTypeRepository CategoryTypes
        {
            get
            {
                return new CategoryTypeRepository(_context);
            }
        }

        public IOperationRepository Operations
        {
            get
            {
                return new OperationRepository(_context);
            }
        }

        public IRecurringPaymentRepository RecurringPayments
        {
            get
            {
                return new RecurringPaymentRepository(_context);
            }
        }
    }
}
