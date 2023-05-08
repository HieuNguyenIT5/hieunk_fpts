using Account.Domain.AggregateModels;
using Account.Domain.DTOs;
using MediatR;

namespace Account.Domain.Events;
public class OrderTotalDomainEvent : IRequest<decimal>
{
    public OrderDto Order { get; set; }

    public OrderTotalDomainEvent(OrderDto order)
    {
        this.Order = order ?? throw new ArgumentNullException(nameof(order));
    }
}
