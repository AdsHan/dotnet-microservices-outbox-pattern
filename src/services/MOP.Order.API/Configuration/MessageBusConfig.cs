using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOP.MessageBus;

namespace MOP.Order.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetConnectionString("RabbitMQCs"));
        }
    }
}