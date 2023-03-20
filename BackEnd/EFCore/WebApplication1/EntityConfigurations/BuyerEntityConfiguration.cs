using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.EntityConfigurations
{
    public class BuyerEntityConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.HasKey(c => c.id);
            builder.Property(c => c.id).IsRequired();

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder
                .Property(c => c.PaymentMethod)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
