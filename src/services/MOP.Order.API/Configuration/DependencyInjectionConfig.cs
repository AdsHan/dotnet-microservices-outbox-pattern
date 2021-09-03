using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOP.Core.Mediator;
using MOP.IntegrationEventLog.Services;
using MOP.Order.API.Application.Messages.Commands.OrderCommand;
using MOP.Order.Domain.Repositories;
using MOP.Order.Infrastructure.Data;
using MOP.Order.Infrastructure.Data.Repositories;

namespace MOP.Order.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Usando com banco de dados em memória
            services.AddDbContext<OrderDbContext>(options => options.UseInMemoryDatabase("OrdersOutbox"));
            //services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SQLServerCs")));

            services.AddScoped<IIntegrationEventLogRepository, IntegrationEventLogRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddMediatR(typeof(CreateOrderCommand));

            return services;
        }
    }
}