using Account.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Infrastructure.EntityConfigurations
{
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder
            .HasKey(p => new { p.OrderId, p.ProductId});
            builder
                .Property(p => p.OrderId)
                .IsRequired();
            builder
                .Property(p => p.ProductId)
                .IsRequired();
            builder
                .HasOne(p => p.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(o => o.OrderId);
            builder
                .HasOne(p => p.Product)
                .WithMany(o => o.OrderItem)
                .HasForeignKey(o => o.OrderId);
        }
    }
}
