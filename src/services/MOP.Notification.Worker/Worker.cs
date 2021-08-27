using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MOP.MessageBus;
using MOP.MessageBus.Integration;
using MOP.Notification.Worker.Data.Repository;
using MOP.Notification.Worker.Services;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOP.Notification.Worker
{
    public class Worker : BackgroundService, IConsumer
    {
        private readonly IMessageBusService _messageBusService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Worker> _logger;

        public Worker(IMessageBusService messageBusService, IServiceProvider serviceProvider, ILogger<Worker> logger)
        {
            _messageBusService = messageBusService;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("**** WORKER Registrado");
            _messageBusService.Subscribe(QueueTypes.NOTIFICATION_ORDER_CREATED, RegisterConsumer);
            return Task.CompletedTask;

        }

        public void RegisterConsumer(BasicDeliverEventArgs message)
        {
            var byteArray = message.Body.ToArray();
            var messageString = Encoding.UTF8.GetString(byteArray);
            var order = JsonConvert.DeserializeObject<OrderCreatedIntegrationEvent>(messageString);

            using (var scope = _serviceProvider.CreateScope())
            {
                var mailRepository = scope.ServiceProvider.GetRequiredService<IMailRepository>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                var t = Task.Run(async () =>
                {
                    var template = await mailRepository.GetTemplate("order-created");

                    if (template != null)
                    {
                        var subject = string.Format(template.Subject, order.Id);
                        var content = string.Format(template.Content, order.Name);

                        await notificationService.SendAsync(subject, content, order.Email, order.Name);
                    }
                });
                t.Wait();
            }
        }
    }
}
