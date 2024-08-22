namespace Operations.Application.Consumers
{
    public interface IMessageConsumer
    {
        int ConsumeMessage(int id);
    }
}
