namespace Accounts.BusinessLogic.Models
{
    public class FinancialAccountModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int AccountTypeId { get; set; }
        public FinancialAccountTypeModel AccountType { get; set; }
        public int CurrencyId { get; set; }
        public CurrencyModel Currency { get; set; }
        public int UserId { get; set; }
        public double Balance { get; set; }
    }
}
