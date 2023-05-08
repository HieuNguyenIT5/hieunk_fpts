using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Domain.AggregateModels;
public class OrderEntityConfiguration : IEntityTypeConfiguration<Orders>
{
    public void Configure(EntityTypeBuilder<Orders> builder)
    {
        builder
            .HasKey(p => p.OrderId);
        builder
            .Property(p => p.CustomerId)
            .IsRequired();
        builder
            .HasOne(p => p.Customer)
            .WithMany(o => o.Orders)
            .HasForeignKey(o => o.CustomerId);
    }
}
