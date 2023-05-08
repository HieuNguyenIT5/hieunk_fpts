namespace Order.Infrastructure.Repositories;
public interface IOrderItemRepository
{
    public void AddOrderItem(int OrderId, int ProductId, int Quantity, decimal Price);
    public int GetLastOrderId();
}
