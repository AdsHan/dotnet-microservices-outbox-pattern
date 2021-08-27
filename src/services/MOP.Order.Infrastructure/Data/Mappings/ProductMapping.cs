using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOP.Order.Domain.Entities;

namespace MOP.Order.Infrastructure.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.BarCode).HasColumnType("VARCHAR(14)").IsRequired();
            builder.Property(p => p.Description).HasColumnType("VARCHAR(60)");
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.ProductGroup).HasConversion<string>();

            // 1 : N => Product : Items
            builder.HasMany(p => p.Items)
                .WithOne(p => p.Product)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Products");
        }
    }
}