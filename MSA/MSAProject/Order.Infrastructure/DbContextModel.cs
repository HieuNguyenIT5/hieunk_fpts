using Order.Domain.AggregateModels;
using Order.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Order.Infrastructure;

public class DbContextModel : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Revenue> Revenues { get; set; }
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
            .ApplyConfiguration(new OrderEntityConfiguration());
        modelBuilder
            .ApplyConfiguration(new OrderItemEntityConfiguration());
        modelBuilder
            .ApplyConfiguration(new ProductEntityConfiguration());
        modelBuilder
            .ApplyConfiguration(new RevenueEntityConfiguration());
    }
}