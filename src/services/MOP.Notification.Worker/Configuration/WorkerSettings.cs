namespace MOP.Notification.Worker.Configuration
{
    public class WorkerSettings
    {
        public string RabbitMQCs { get; set; }
        public string MongoDBCs { get; set; }
        public string NotificationFromEmail { get; set; }
        public string NotificationFromName { get; set; }
    }
}
