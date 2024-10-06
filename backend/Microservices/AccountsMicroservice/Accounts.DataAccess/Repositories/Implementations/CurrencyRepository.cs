using Accounts.DataAccess.Data;
using Accounts.DataAccess.Entities;
using Accounts.DataAccess.Repositories.Interfaces;

namespace Accounts.DataAccess.Repositories.Implementations
{
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(FinancialAccountsDbContext context) : base(context)
        {

        }
    }
}
