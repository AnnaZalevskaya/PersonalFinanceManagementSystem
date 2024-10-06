namespace Accounts.BusinessLogic.Settings
{
    public class RabbitMQSettings
    {
        public string? Uri { get; init; }
        public string? SendingQueue { get; init; }
        public string? ReceivingQueue { get; init; }
    }
}
