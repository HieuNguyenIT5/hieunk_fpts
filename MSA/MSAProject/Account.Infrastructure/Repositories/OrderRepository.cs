using Account.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Account.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DbContextModel _dbContext;
    public OrderRepository(DbContextModel dbContext)
    {
        this._dbContext = dbContext;
    }
    public List<Orders> GetOrderByStatus(int status)
    {
        List<Orders> orders = _dbContext.Orders.Where(o => o.STATUS == status).ToList();
        foreach (var order in orders)
        {
            List<OrderItem> items = (
            from i in _dbContext.OrderItems
            join p in _dbContext.Products on i.ProductId equals p.ProductId
            where i.OrderId == order.OrderId
            select new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }
            ).ToList();
            order.Items = items;
        }
        return orders;
    }
    public List<Orders> getOrderByCustomerId(string cus_id)
    {
        var orders = _dbContext.Orders.Where(o => o.CustomerId == cus_id).ToList();
        foreach (var order in orders)
        {
            List<OrderItem> items = (
            from i in _dbContext.OrderItems
            join p in _dbContext.Products on i.ProductId equals p.ProductId
            where i.OrderId == order.OrderId
            select new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }
            ).ToList();
            order.Items = items;
        }
        return orders;
    }
}
