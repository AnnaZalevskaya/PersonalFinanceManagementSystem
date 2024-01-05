namespace Accounts.DataAccess.Entities
{
    public class Currency
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Abbreviation { get; set; }
        public List<FinancialAccount>? FinancialAccounts { get; set; }
    }
}
