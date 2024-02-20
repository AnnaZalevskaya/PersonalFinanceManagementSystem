using Operations.Application.Interfaces;
using Operations.Infrastructure.Data;

namespace Operations.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OperationsDbContext _context;

        public UnitOfWork(OperationsDbContext context)
        {
            _context = context;
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
    }
}
