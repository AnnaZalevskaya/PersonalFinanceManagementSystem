using Accounts.DataAccess.Data;
using Accounts.DataAccess.Entities;
using Accounts.DataAccess.Repositories.Interfaces;

namespace Accounts.DataAccess.Repositories.Implementations
{
    public class FinancialGoalTypeRepository : BaseRepository<FinancialGoalType>, IFinancialGoalTypeRepository
    {
        public FinancialGoalTypeRepository(FinancialAccountsDbContext context) : base(context)
        {

        }
    }
}
