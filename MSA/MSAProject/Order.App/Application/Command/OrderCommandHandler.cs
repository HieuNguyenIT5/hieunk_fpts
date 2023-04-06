using MediatR;
using Order.Domain.AggregateModels;
using Order.Infrastructure;
using System.Text.Json;

namespace Order.App.Application.Command;

public class OrderCommandHandler : IRequestHandler<OrderCommand>
{
    private readonly DbContextModel _db;
    private readonly IMediator _mediator;
    public OrderCommandHandler(DbContextModel db, IMediator mediator)
    {
        this._db = db;
        this._mediator = mediator;
    }
    public Task Handle(OrderCommand request, CancellationToken cancellationToken)
    {
        var json = request.consumer.Message.Value;
        dynamic message = JsonSerializer.Deserialize<JsonElement>(json);
        bool success = message.GetProperty("success").GetBoolean();
        if (success)
        {
            List<OrderItem> data = new List<OrderItem>();
            data = JsonSerializer.Deserialize<List<OrderItem>>(message.GetProperty("data"));
            bool check = true;
            foreach (var item in data)
            {
                var product = _db.Products.Find(item.ProductId);
                if (product != null)
                {
                    check = product.checkQuantity(item.Quantity);
                }
                else
                {
                    check = false;
                }
            }
            if (check)
            {
                foreach (var item in data)
                {
                    var product = _db.Products.Find(item.ProductId);
                    OrderItem orderItem = new OrderItem(item.CustomerId, item.ProductId, item.Quantity, item.Price);
                    _db.OrderItem.Add(orderItem);
                    _db.SaveChanges();
                    product.Quantity -= item.Quantity;
                    product.QuantitySold += item.Quantity;

                    OrderItem order = _db.OrderItem.OrderByDescending(r => r.OrderId).LastOrDefault();

                    Revenue revenue = new Revenue(order.OrderId, item.SubTotal());
                    _db.Revenue.Add(revenue);

                    Customer customer = _db.Customers.Find(item.CustomerId);
                    customer.CustomerWallet -= item.SubTotal();
                    _db.SaveChanges();
                }
            }
            else
            {
               _mediator.Send(new SendMailCommand("Số lượng sản phẩm không đủ", "hieukhac6869@gmail.com"));
            }
        }else
        {
            _mediator.Send(new SendMailCommand("Thành công", "hieukhac6869@gmail.com"));
        }
        return Task.CompletedTask;

    }
}
