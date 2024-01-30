using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace Auth.Application.Producers
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
        }

        public void SendMessage(object messageObject, string id)
        {
            _channel.QueueDeclare(queue: "users_queue_" + id,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            var messageJson = JsonConvert.SerializeObject(messageObject);
            var body = Encoding.UTF8.GetBytes(messageJson);

            _channel.BasicPublish(exchange: "",
                                 routingKey: "users_queue_" + id,
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
