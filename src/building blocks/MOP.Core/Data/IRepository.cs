using MOP.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOP.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task SaveAsync();
        void Update(T obj);
        void Add(T obj);
    }
}