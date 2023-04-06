using Order.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Infrastructure.EntityConfigurations;

public class RevenueEntityConfiguration : IEntityTypeConfiguration<Revenue>
{
    public void Configure(EntityTypeBuilder<Revenue> builder)
    {
        builder
            .HasKey(p => p.RevenueId);
        //builder
        //    .HasOne(p => p.Order)
        //    .WithOne(p => p.Revenue)
        //    .HasForeignKey<Revenue>(r => r.OrderId);
    }
}
