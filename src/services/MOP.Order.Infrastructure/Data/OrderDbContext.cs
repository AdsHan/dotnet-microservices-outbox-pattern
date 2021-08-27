using Microsoft.EntityFrameworkCore;
using MOP.Order.Domain.Entities;
using System.Reflection;

namespace MOP.Order.Infrastructure.Data
{
    public class OrderDbContext : DbContext
    {

        public OrderDbContext()
        {

        }

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }

        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
