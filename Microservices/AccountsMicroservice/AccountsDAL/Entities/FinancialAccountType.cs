namespace Accounts.DataAccess.Entities
{
    public class FinancialAccountType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<FinancialAccount>? FinancialAccounts { get; set; }
    }
}
