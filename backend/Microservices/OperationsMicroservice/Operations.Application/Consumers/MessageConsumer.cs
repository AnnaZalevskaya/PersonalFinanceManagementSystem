using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using Operations.Application.Models.Consts;

namespace Operations.Application.Consumers
{
    public class MessageConsumer : IMessageConsumer
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageConsumer()
        {
            _factory = new ConnectionFactory() { Uri = new Uri(RabbitMQConsts.Uri) };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: RabbitMQConsts.ReceivingQueue,
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);
        }

        public int ConsumeMessage(int id)
        {
            var message = GetMessageFromQueue(msg => msg.Id == id);

            if (message != null)
            {
                var messageObject = JsonConvert.DeserializeObject<dynamic>(message);
                Console.WriteLine(" [x] Received id: {0}", messageObject?.Id);

                return messageObject?.Id;
            }

            return 0;
        }

        private string GetMessageFromQueue(Func<dynamic, bool> filter)
        {
            BasicGetResult result = _channel.BasicGet(RabbitMQConsts.ReceivingQueue, true);

            if (result != null)
            {
                var body = result.Body.ToArray();
                var messageObject = JsonConvert.DeserializeObject<dynamic>(Encoding.UTF8.GetString(body));

                if (filter(messageObject))
                {
                    return Encoding.UTF8.GetString(body);
                }
            }

            return null;
        }
    }
}
