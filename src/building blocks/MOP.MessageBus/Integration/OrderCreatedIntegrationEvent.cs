using System;

namespace MOP.MessageBus.Integration
{
    public class OrderCreatedIntegrationEvent : Event
    {
        public OrderCreatedIntegrationEvent(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}