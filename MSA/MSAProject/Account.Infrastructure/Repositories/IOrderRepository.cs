using Account.Domain.AggregateModels;

namespace Account.Infrastructure.Repositories;
public interface IOrderRepository
{
    public List<Order> GetOrderByStatus(int status);
    public List<Order> getOrderByCustomerId(string cus_id);
}
