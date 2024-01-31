using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Accounts.BusinessLogic.Producers
{
    public class MessageProducer : IMessageProducer
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageProducer()
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "accounts_queue",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void SendMessages(IEnumerable<object> messageObjects)
        {
            foreach (var messageObject in messageObjects)
            {
                var messageJson = JsonConvert.SerializeObject(messageObject);
                var body = Encoding.UTF8.GetBytes(messageJson);
                _channel.BasicPublish(exchange: "",
                                     routingKey: "accounts_queue",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent obj: {0}", messageObject);
            }
        }

        public void Close()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
