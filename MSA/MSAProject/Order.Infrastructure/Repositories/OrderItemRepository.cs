using Order.Domain.AggregateModels;

namespace Order.Infrastructure.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly DbContextModel _dbContext;
    public OrderItemRepository(DbContextModel dbContext)
    {
        this._dbContext = dbContext;
    }
    public void AddOrderItem(int OrderId, int ProductId, int Quantity, decimal Price)
    {
        var newOrderItem = new OrderItem();
        newOrderItem.OrderId = OrderId;
        newOrderItem.ProductId = ProductId;
        newOrderItem.Quantity = Quantity;
        newOrderItem.Price = Price;
        _dbContext.SaveChanges();
    }
    public int GetLastOrderId()
    {
        var order = _dbContext.OrderItems.OrderByDescending(r => r.OrderId).FirstOrDefault();
        int id = order != null ? order.OrderId : 0;
        return id;
    }
}
