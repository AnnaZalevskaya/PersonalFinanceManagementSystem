namespace Auth.Application.Producers
{
    public interface IMessageProducer
    {
        void SendMessage(object messageObject, string id);
    }
}
