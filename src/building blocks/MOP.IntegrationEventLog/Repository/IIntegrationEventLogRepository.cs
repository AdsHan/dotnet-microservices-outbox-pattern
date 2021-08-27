using MongoDB.Driver;
using MOP.IntegrationEventLog.Entities;
using MOP.MessageBus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOP.IntegrationEventLog.Services
{
    public interface IIntegrationEventLogRepository
    {
        Task<List<IntegrationEventLogModel>> GetAllAsync();
        Task<IntegrationEventLogModel> GetByIdAsync(Guid id);
        Task<Guid> SaveAsync(Guid aggregateRootId, string aggregateRootType, Event @event);
        Task<ReplaceOneResult> UpdateAsync(IntegrationEventLogModel @event);
        Task<DeleteResult> DeleteAsync(Guid id);
        Task MarkEventAsPublishedAsync(Guid eventId);
        Task MarkEventAsNoPublishedAsync(Guid eventId);
    }
}
