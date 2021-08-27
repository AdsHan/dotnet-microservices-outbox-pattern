using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOP.Order.Domain.Entities;

namespace MOP.Order.Infrastructure.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<CustomerModel>
    {
        public void Configure(EntityTypeBuilder<CustomerModel> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(p => p.Name).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(p => p.Phone).HasColumnType("CHAR(11)");
            builder.Property(p => p.Email).HasColumnType("CHAR(254)").IsRequired();
            builder.Property(p => p.CEP).HasColumnType("CHAR(8)").IsRequired();
            builder.Property(p => p.State).HasColumnType("CHAR(2)").IsRequired();
            builder.Property(p => p.City).HasMaxLength(60).IsRequired();

            builder.HasIndex(i => i.Phone).HasName("idx_customer_phone");

            // 1 : N => Customer : Orders
            builder.HasMany(p => p.Orders)
                .WithOne(p => p.Customer)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Customers");
        }
    }
}