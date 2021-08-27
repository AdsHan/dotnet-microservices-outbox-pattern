using MOP.IntegrationEventLog.Enums;
using MOP.MessageBus;
using System;
using System.Text.Json;

namespace MOP.IntegrationEventLog.Entities
{
    public class IntegrationEventLogModel
    {

        public IntegrationEventLogModel(Guid aggregateRootId, string aggregateRootType, Event evt)
        {
            Id = new Guid();
            AggregateRootId = aggregateRootId;
            AggregateRootType = aggregateRootType;
            EventType = evt.GetType().Name;
            EventBody = JsonSerializer.Serialize(evt, evt.GetType());
            PublishStatus = IntegrationEventStatusEnum.Created;
        }

        public Guid Id { get; set; }
        public Guid AggregateRootId { get; private set; }
        public string AggregateRootType { get; private set; }
        public string EventType { get; private set; }
        public string EventBody { get; private set; }
        public IntegrationEventStatusEnum PublishStatus { get; set; }
    }
}
