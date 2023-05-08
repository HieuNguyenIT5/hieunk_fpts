using Account.Domain.AggregateModels;
using Account.Domain.DTOs;
using MediatR;

namespace Account.Domain.Events;
public class CheckCashCustomerDomainEvent :IRequest<string>
{
    public decimal orderTotal{ get; set; }
    public OrderDto order { get; set; }

    public CheckCashCustomerDomainEvent(decimal orderTotal, OrderDto order)
    {
        this.orderTotal = orderTotal;
        this.order = order;
    }
}
