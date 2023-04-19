using Account.Domain.AggregateModels;
using MediatR;

namespace Account.Domain.Events;
public class CheckCashCustomerDomainEvent :IRequest<string>
{
    public decimal orderTotal{ get; set; }
    public List<Order> orders { get; set; }

    public CheckCashCustomerDomainEvent(decimal orderTotal, List<Order> orders)
    {
        this.orderTotal = orderTotal;
        this.orders = orders;
    }
}
