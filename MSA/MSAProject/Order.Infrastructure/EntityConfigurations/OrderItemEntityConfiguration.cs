using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Domain.AggregateModels;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using Order.Domain.AggregateModels;

namespace Account.Domain.AggregateModels;
public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder
            .HasKey(p => p.OrderId);
        builder
            .Property(p => p.CustomerId)
            .IsRequired();
        builder
            .Property (p => p.ProductId)
            .IsRequired();
        //builder
        //    .HasOne(p => p.Product)
        //    .WithMany( o => o.Orders)
        //    .HasForeignKey(o => o.ProductId);
        //builder
        //    .HasOne(p => p.Customer)
        //    .WithMany(o => o.Orders)
        //    .HasForeignKey(o => o.CustomerId);
    }
}
