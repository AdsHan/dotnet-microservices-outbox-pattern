using System;

namespace MOP.Notification.Worker.Data.Model
{
    public class EmailTemplateModel
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Event { get; set; }
    }
}