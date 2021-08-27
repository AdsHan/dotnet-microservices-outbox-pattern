using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOP.Order.Domain.Entities;

namespace MOP.Order.Infrastructure.Data.Mappings
{
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItemModel>
    {
        public void Configure(EntityTypeBuilder<OrderItemModel> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(p => p.Quantity).HasDefaultValue(1).IsRequired();
            builder.Property(p => p.UnitPrice).HasDefaultValue(0).IsRequired();
            builder.Property(p => p.Discount).HasConversion<int>();

            // N : 1 => Item : Orders
            builder.HasOne(t => t.Order)
                .WithMany(e => e.Items)
                .OnDelete(DeleteBehavior.Restrict);

            // N : 1 => Item : Produto
            builder.HasOne(t => t.Product)
                .WithMany(e => e.Items)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("OrderItems");
        }
    }
}