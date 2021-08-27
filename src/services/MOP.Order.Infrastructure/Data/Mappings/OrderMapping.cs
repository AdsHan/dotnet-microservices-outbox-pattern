using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOP.Order.Domain.Entities;

namespace MOP.Order.Infrastructure.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<OrderModel>
    {
        public void Configure(EntityTypeBuilder<OrderModel> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(p => p.StartedIn).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(p => p.Status).HasConversion<int>();
            builder.Property(p => p.Shipping).HasConversion<string>();
            builder.Property(p => p.Observation).HasColumnType("VARCHAR(512)");

            // N : 1 => Order : Customer
            builder.HasOne(t => t.Customer)
                .WithMany(e => e.Orders)
                .OnDelete(DeleteBehavior.Restrict);

            // 1 : N => Order : Items
            builder.HasMany(p => p.Items)
                .WithOne(p => p.Order)
                .HasForeignKey(c => c.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Orders");
        }
    }
}