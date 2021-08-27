using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using MOP.MessageBus;
using MOP.Notification.Worker.Configuration;
using MOP.Notification.Worker.Data.Repository;
using MOP.Notification.Worker.Services;

namespace MOP.Notification.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
            {
                IConfiguration configuration = hostContext.Configuration;
                WorkerSettings options = configuration.GetSection("Settings").Get<WorkerSettings>();
                services.AddSingleton(options);

                services.AddSingleton<IMongoClient>(sp =>
                {
                    return new MongoClient(options.MongoDBCs);
                });
                services.AddTransient(sp =>
                {
                    BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
                    var mongoClient = sp.GetService<IMongoClient>();
                    return mongoClient.GetDatabase("notification-worker");
                });

                services.AddScoped<IMailRepository, MailRepository>();
                services.AddTransient<INotificationService, NotificationService>();
                services.AddMessageBus(options.RabbitMQCs);
                services.AddHostedService<Worker>();
            });
    }
}
