using Account.Domain.AggregateModels;

namespace Account.Infrastructure.Repositories;
public interface IOrderRepository
{
    public List<Orders> GetOrderByStatus(int status);
    public List<Orders> getOrderByCustomerId(string cus_id);
}
