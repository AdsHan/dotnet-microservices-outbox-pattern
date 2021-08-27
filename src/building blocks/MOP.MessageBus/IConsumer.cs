using RabbitMQ.Client.Events;

namespace MOP.MessageBus
{
    public interface IConsumer
    {
        void RegisterConsumer(BasicDeliverEventArgs message);
    }
}
