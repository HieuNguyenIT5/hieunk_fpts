namespace Order.Infrastructure.Repositories;
public interface IOrderItemRepository
{
    public void AddOrderItem(string CustomerId, int ProductId, int Quantity, decimal Price, string IP, int status);
    public int GetLastOrderId();
}
