using Account.Domain.AggregateModels;
using Account.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Account.App.Application.DomainEventHandlers
{
    public class OrderTotalDomainEventHandler : IRequestHandler<OrderTotalDomainEvent, decimal>
    {
       public async Task<decimal> Handle(OrderTotalDomainEvent request, CancellationToken cancellationToken)
        {
            decimal total = 0;
            foreach(var order in request.Orders)
            {
                total += order.SubTotal();
            }
            return total;
        }
    }
}
