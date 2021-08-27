using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace MOP.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly string _connectionString;

        private ConnectionFactory _factory;
        private IModel _channel;

        public MessageBusService(string connectionString)
        {
            _connectionString = connectionString;
            CreateBus();
        }

        public void Publish<T>(string exchange, string queue, T message) where T : Event
        {
            createStructure(queue, exchange);

            // Cria mensagem e envia para fila 
            var contentJson = JsonSerializer.Serialize<object>(message);
            var contentBytes = Encoding.UTF8.GetBytes(contentJson);

            _channel.BasicPublish(exchange: exchange, routingKey: queue, basicProperties: null, body: contentBytes);
        }

        public void Subscribe(string queue, Action<BasicDeliverEventArgs> callback)
        {
            createStructure(queue);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                callback(eventArgs);
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(queue, false, consumer);
        }


        public void createStructure(string queue, string exchange = null)
        {
            if (!string.IsNullOrEmpty(exchange))
            {
                _channel.ExchangeDeclare(exchange, "topic", true);
                _channel.QueueBind(queue, exchange, queue);
            }

            _channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        private void CreateBus()
        {
            _factory = new ConnectionFactory();
            _factory.Uri = new Uri(_connectionString);
            _channel = _factory.CreateConnection().CreateModel();
        }

    }
}
