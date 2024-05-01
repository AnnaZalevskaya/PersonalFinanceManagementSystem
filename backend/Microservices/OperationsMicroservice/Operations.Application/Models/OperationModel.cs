namespace Operations.Application.Models
{
    public class OperationModel
    {
        public string Id { get; set; }
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, object> Description { get; set; }
    }
}
