using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using Accounts.BusinessLogic.Models;

namespace Accounts.BusinessLogic.Consumers
{
    public class MessageConsumer : IMessageConsumer
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageConsumer()
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "message_queue",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public int ConsumeMessage()
        {
            int response = 0;
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageJson = Encoding.UTF8.GetString(body);
                var messageObject = JsonConvert.DeserializeObject<dynamic>(messageJson);
                Console.WriteLine(" [x] Received id: {0}", messageObject.Id);
                response = messageObject.Id;
            };
            _channel.BasicConsume(queue: "message_queue",
                                 autoAck: true,
                                 consumer: consumer);

            return response;
        }

        public void Close()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
