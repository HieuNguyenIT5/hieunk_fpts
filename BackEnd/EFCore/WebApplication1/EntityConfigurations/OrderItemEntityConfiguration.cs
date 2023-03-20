using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.EntityConfigurations
{
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builer)
        {
            builer
                .Property(c => c.OrderId)
                .IsRequired();
            builer
                .Property(c => c.ProductId)
                .IsRequired();
            builer
                .Property(c => c.Units)
                .IsRequired();
            builer
                .Property(c => c.UnitPrice)
                .IsRequired();
            builer
                .HasOne(c => c.Order)
                .WithMany(d => d.OrderItems)
                .HasForeignKey(c => c.OrderId)
                .HasPrincipalKey(d => d.id)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

        }
    }
}
