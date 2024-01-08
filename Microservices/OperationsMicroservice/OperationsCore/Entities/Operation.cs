namespace Operations.Core.Entities
{
    public class Operation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
    }
}
