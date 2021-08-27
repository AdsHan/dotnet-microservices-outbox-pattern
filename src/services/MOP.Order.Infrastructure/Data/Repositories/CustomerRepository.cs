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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrderDbContext _dbContext;

        public CustomerRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CustomerModel>> GetAllAsync()
        {
            return await _dbContext.Customers
                .Where(a => a.Status == EntityStatusEnum.Active)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CustomerModel> GetByIdAsync(Guid id)
        {
            return await _dbContext.Customers
                .Where(a => a.Status == EntityStatusEnum.Active)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Update(CustomerModel customer)
        {
            _dbContext.Update(customer);
        }

        public void Add(CustomerModel custome)
        {
            _dbContext.Add(custome);
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
