using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.EntityConfigurations;

public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builer)
    {
        builer
            .Property(c => c.OrderId)
            .IsRequired();
        builer.HasKey(o => new { o.OrderId, o.ProductId });
        builer
            .Property(c => c.ProductId)
            .IsRequired();
        builer
            .Property(c => c.Units)
            .IsRequired();
        builer
            .Property(c => c.UnitPrice)
            .IsRequired();
    }
}
