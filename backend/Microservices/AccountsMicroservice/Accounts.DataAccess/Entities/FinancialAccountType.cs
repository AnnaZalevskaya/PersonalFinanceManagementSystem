using Accounts.DataAccess.Entities.Base;

namespace Accounts.DataAccess.Entities
{
    public class FinancialAccountType : BaseEntity
    {
        public List<FinancialAccount>? FinancialAccounts { get; set; }
    }
}
