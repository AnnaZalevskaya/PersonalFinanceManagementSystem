using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using Auth.Application.Models.Consts;

namespace Auth.Application.Producers
{
    public class MessageProducer : IMessageProducer
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageProducer()
        {
            _factory = new ConnectionFactory() { Uri = new Uri(RabbitMQConsts.Uri) };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: RabbitMQConsts.SendingQueue,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void SendMessage(object messageObject)
        {
            var messageJson = JsonConvert.SerializeObject(messageObject);
            var body = Encoding.UTF8.GetBytes(messageJson);

            _channel.BasicPublish(exchange: "",
                                 routingKey: RabbitMQConsts.SendingQueue,
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine(" [x] Sent obj: {0}", messageObject);
        }

        public void Close()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
