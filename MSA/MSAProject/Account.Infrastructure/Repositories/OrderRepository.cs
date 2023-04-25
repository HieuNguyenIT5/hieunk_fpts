using Account.Domain.AggregateModels;

namespace Account.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DbContextModel _dbContext;
    public OrderRepository(DbContextModel dbContext)
    {
        this._dbContext = dbContext;
    }
    public List<Order> GetOrderByStatus(int status)
    {
        return _dbContext.Orders.Where(o => o.STATUS == status).ToList();
    }
    public List<Order> getOrderByCustomerId(string cus_id)
    {
        return _dbContext.Orders.Where(o => o.CustomerId == cus_id).ToList();
    }
}
