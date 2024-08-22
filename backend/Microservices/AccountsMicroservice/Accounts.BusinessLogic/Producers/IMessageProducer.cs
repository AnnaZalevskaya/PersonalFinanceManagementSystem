namespace Accounts.BusinessLogic.Producers
{
    public interface IMessageProducer
    {
        void SendMessages(IEnumerable<object> messageObjects);
    }
}
