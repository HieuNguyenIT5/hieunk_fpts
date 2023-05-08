using Account.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Account.Infrastructure.EntityConfigurations;

public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.CustomerId);
        builder
            .Property(x => x.CustomerName)
            .IsRequired();
        builder
            .Property(x => x.CustomerEmail)
            .IsRequired();
        builder
            .Property(e => e.CustomerName)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(true);
    }
}
