using Accounts.DataAccess.Data;
using Accounts.DataAccess.Repositories.Implementations;
using Accounts.DataAccess.Repositories.Interfaces;

namespace Accounts.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinancialAccountsDbContext _context;

        private IFinancialAccountRepository? _financialAccountRepository;
        private IFinancialAccountTypeRepository? _financialAccountTypeRepository;
        private ICurrencyRepository? _currencyRepository;

        public UnitOfWork(FinancialAccountsDbContext context)
        {
            _context = context;
        }

        public IFinancialAccountRepository FinancialAccounts
        {
            get
            {
                _financialAccountRepository ??= new FinancialAccountRepository(_context);
                return _financialAccountRepository;
            }
        }

        public IFinancialAccountTypeRepository FinancialAccountTypes
        {
            get
            {
                _financialAccountTypeRepository ??= new FinancialAccountTypeRepository(_context);
                return _financialAccountTypeRepository;
            }
        }

        public ICurrencyRepository Currencies
        {
            get
            {
                _currencyRepository ??= new CurrencyRepository(_context);
                return _currencyRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
