namespace Accounts.BusinessLogic.Consumers
{
    public interface IMessageConsumer
    {
        int ConsumeMessage(int id);
    }
}
