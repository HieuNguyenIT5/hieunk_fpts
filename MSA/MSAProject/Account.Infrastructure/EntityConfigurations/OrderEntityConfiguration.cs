using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Domain.AggregateModels;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Account.Domain.AggregateModels;
public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
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
