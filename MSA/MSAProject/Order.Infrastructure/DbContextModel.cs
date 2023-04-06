using Order.Domain.AggregateModels;
using Order.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Account.Domain.AggregateModels;

namespace Order.Infrastructure;
public class DbContextModel : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    public DbSet<Revenue> Revenue { get; set; }
    //public DbContextModel(DbContextOptions<DbContextModel> options) : base(options)
    //{

    //}
    public DbContextModel(DbContextOptions options) : base(options)
    {
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
            .ApplyConfiguration(new ProductEntityConfiguration());
        modelBuilder
            .ApplyConfiguration(new RevenueEntityConfiguration());
        modelBuilder
            .ApplyConfiguration(new OrderItemEntityConfiguration());
    }
}
