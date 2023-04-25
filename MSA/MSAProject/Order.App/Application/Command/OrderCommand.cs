namespace Order.App.Application.Command;
public class OrderCommand : IRequest
{
    public List<OrderItem> Data{ get; set; }
    public OrderCommand(List<OrderItem> data)
    {
        this.Data = data;
    }
}
