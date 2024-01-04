namespace Accounts.DataAccess.Entities
{
    public class FinancialAccount
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int AccountTypeId { get; set; }
        public FinancialAccountType? AccountType { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public int UserId { get; set; }
    }
}
