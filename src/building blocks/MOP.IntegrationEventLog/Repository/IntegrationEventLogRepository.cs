using MongoDB.Driver;
using MOP.IntegrationEventLog.Entities;
using MOP.IntegrationEventLog.Enums;
using MOP.MessageBus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOP.IntegrationEventLog.Services
{
    public class IntegrationEventLogRepository : IIntegrationEventLogRepository
    {

        private readonly IMongoCollection<IntegrationEventLogModel> _collection;

        public IntegrationEventLogRepository(IMongoDatabase mongoDatabase)
        {
            _collection = mongoDatabase.GetCollection<IntegrationEventLogModel>("IntegrationEvent");
        }

        public async Task<List<IntegrationEventLogModel>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync(); ;
        }

        public async Task<IntegrationEventLogModel> GetByIdAsync(Guid id)
        {
            return await _collection.Find(c => c.Id == id).SingleOrDefaultAsync();
        }

        public async Task<Guid> SaveAsync(Guid aggregateRootId, string aggregateRootType, Event @event)
        {
            var newEvent = new IntegrationEventLogModel(aggregateRootId, aggregateRootType, @event);

            await _collection.InsertOneAsync(newEvent);

            return newEvent.Id;
        }

        public async Task<ReplaceOneResult> UpdateAsync(IntegrationEventLogModel @event)
        {
            return await _collection.ReplaceOneAsync(c => c.Id.Equals(@event.Id), @event);
        }

        public async Task<DeleteResult> DeleteAsync(Guid id)
        {
            return await _collection.DeleteOneAsync(c => c.Id == id);
        }

        public Task MarkEventAsPublishedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, IntegrationEventStatusEnum.Published);
        }

        public Task MarkEventAsNoPublishedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, IntegrationEventStatusEnum.NoPublished);
        }

        private async Task UpdateEventStatus(Guid eventId, IntegrationEventStatusEnum status)
        {
            if (eventId == null) return;

            var evt = await _collection.Find(c => c.Id == eventId).SingleOrDefaultAsync();

            if (evt == null) return;

            evt.PublishStatus = status;

            await _collection.ReplaceOneAsync(c => c.Id.Equals(evt.Id), evt);
        }
    }
}

