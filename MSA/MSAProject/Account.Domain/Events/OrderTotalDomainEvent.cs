using Account.Domain.AggregateModels;
using MediatR;

namespace Account.Domain.Events;
public class OrderTotalDomainEvent : IRequest<decimal>
{
    public List<Order> Orders { get; set; }

    public OrderTotalDomainEvent(List<Order> orders)
    {
        this.Orders = orders ?? throw new ArgumentNullException(nameof(orders));
    }
}
