using Accounts.DataAccess.Entities.Base;

namespace Accounts.DataAccess.Entities
{
    public class Currency : BaseEntity
    {
        public string? Abbreviation { get; set; }
        public string? Sign { get; set; }
        public List<FinancialAccount>? FinancialAccounts { get; set; }
    }
}
