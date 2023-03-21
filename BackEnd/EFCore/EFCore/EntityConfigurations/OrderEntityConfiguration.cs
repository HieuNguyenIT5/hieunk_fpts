using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.EntityConfigurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(c => c.id);
        builder
            .Property(c => c.Status)
            .IsRequired();
        builder
            .Property(c => c.BuyerId)
            .IsRequired();
        builder
            .Property(c => c.address)
            .IsRequired()
            .HasMaxLength(500);
    }
}
