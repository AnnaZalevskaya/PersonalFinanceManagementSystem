using Accounts.DataAccess.Data;
using Accounts.DataAccess.Entities;
using Accounts.DataAccess.Repositories.Interfaces;

namespace Accounts.DataAccess.Repositories.Implementations
{
    public class FinancialAccountTypeRepository : BaseRepository<FinancialAccountType>, IFinancialAccountTypeRepository
    {
        public FinancialAccountTypeRepository(FinancialAccountsDbContext context) : base(context)
        {
            
        }
    }
}
