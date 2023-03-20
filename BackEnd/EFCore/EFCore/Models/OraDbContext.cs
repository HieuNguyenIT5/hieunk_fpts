using WebApplication1.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;
using System.Diagnostics;

namespace WebApplication1.Models
{
    public class OraDbContext : DbContext
    {
        public DbSet<Buyer> Buyer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public OraDbContext(DbContextOptions<OraDbContext> options):base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new BuyerEntityConfiguration())
                .ApplyConfiguration(new OrderEntityConfiguration())
                .ApplyConfiguration(new OrderItemEntityConfiguration());
        }
    }
}
