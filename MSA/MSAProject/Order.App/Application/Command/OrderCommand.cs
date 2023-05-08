using Order.Domain.DTOs;

namespace Order.App.Application.Command;
public class OrderCommand : IRequest
{
    public OrderDto Data{ get; set; }
    public OrderCommand(OrderDto data)
    {
        this.Data = data;
    }
}
