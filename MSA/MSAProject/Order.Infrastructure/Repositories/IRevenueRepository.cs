namespace Order.Infrastructure.Repositories;

public interface IRevenueRepository
{
    public void Add(int orderId, decimal total);
}
