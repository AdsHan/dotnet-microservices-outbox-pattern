using Microsoft.EntityFrameworkCore;
using MOP.Core.Enums;
using MOP.Order.Domain.Entities;
using MOP.Order.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOP.Order.Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderModel>> GetAllAsync()
        {
            return await _dbContext.Orders
                .Where(a => a.Status == EntityStatusEnum.Active)
                .Include(x => x.Customer)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<OrderModel> GetByIdAsync(Guid id)
        {
            return await _dbContext.Orders
                .Where(a => a.Status == EntityStatusEnum.Active)
                .Include(x => x.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Update(OrderModel order)
        {
            _dbContext.Update(order);
        }

        public void Add(OrderModel order)
        {
            _dbContext.Add(order);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}
