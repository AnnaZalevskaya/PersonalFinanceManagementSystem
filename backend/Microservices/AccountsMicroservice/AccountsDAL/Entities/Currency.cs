namespace Accounts.DataAccess.Entities
{
    public class Currency : BaseEntity
    {
        public string? Abbreviation { get; set; }
        public List<FinancialAccount>? FinancialAccounts { get; set; }
    }
}
