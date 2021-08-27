using RabbitMQ.Client.Events;
using System;

namespace MOP.MessageBus
{
    public interface IMessageBusService
    {
        public void Publish<T>(string exchange, string queue, T message) where T : Event;
        public void Subscribe(string queue, Action<BasicDeliverEventArgs> callback);
    }
}
