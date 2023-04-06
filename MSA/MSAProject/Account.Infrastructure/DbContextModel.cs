using Account.Domain.AggregateModels;
using Account.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure
{
    public class DbContextModel : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Revenue> Revenue { get; set; }
        public DbContextModel(DbContextOptions<DbContextModel> options):base(options) {
        
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new CustomerEntityConfiguration());
            modelBuilder
                .ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder
                .ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder
                .ApplyConfiguration(new RevenueEntityConfiguration());
        }



    }
}
