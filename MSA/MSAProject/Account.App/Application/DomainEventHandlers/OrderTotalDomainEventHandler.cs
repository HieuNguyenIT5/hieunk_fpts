namespace Account.App.Application.DomainEventHandlers
{
    public class OrderTotalDomainEventHandler : IRequestHandler<OrderTotalDomainEvent, decimal>
    {
       public async Task<decimal> Handle(OrderTotalDomainEvent request, CancellationToken cancellationToken)
        {
            decimal total = 0;
            foreach(var order in request.Order.Items)
            {
                total += 1;
            }
            return total;
        }
    }
}
