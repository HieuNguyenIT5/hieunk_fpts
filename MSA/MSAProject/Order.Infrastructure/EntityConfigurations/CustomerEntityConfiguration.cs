using Order.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Order.Infrastructure.EntityConfigurations;

public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.CustomerId);
        builder
            .HasKey(e => e.CustomerId);
        builder
            .Property(e => e.CustomerName)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(true);
    }
}
