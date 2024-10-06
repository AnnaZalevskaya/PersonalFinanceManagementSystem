namespace Operations.Application.Settings
{
    public class RabbitMQSettings
    {
        public string? Uri { get; init; }
        public string? ReceivingQueue { get; init; }
    }
}
