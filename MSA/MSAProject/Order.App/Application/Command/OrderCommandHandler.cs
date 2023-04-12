using MediatR;
using Order.App.Services;
using Order.Domain.AggregateModels;
using Order.Infrastructure;
using Order.Infrastructure.Repositories;
using Order.App.Common;
using System.Text.Json;

namespace Order.App.Application.Command;

public class OrderCommandHandler : IRequestHandler<OrderCommand>
{
    private readonly DbContextModel _db;
    private readonly IMediator _mediator;
    private readonly IProductRepository _productRepo;
    private readonly IOrderItemRepository _orderItemRepo;
    private readonly IRevenueRepository _revenueRepo;
    private readonly ICustomerRepository _customerRepo;
    private readonly Constants _constants;
    public OrderCommandHandler(DbContextModel db, 
        IMediator mediator, 
        IProductRepository productRepo, 
        IOrderItemRepository orderItemRepo, 
        IRevenueRepository revenueRepo,
        ICustomerRepository customerRepo,
        Constants constants
     )
    {
        this._db = db;
        this._mediator = mediator;
        this._productRepo = productRepo;
        _orderItemRepo = orderItemRepo;
        _revenueRepo = revenueRepo;
        _customerRepo = customerRepo;
        _constants = constants;
    }

    public Task<Unit> Handle(OrderCommand request, CancellationToken cancellationToken)
    {
        var json = request.consumer.Message.Value;
        dynamic message = JsonSerializer.Deserialize<JsonElement>(json);
        bool success = message.GetProperty("success").GetBoolean();
        MailRequest mailRequest;
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
                string body = "";
                decimal totalCash = 0;
                foreach (var item in data)
                {
                    var subTotal = item.SubTotal();
                    _orderItemRepo.AddOrderItem(item.CustomerId, item.ProductId, item.Quantity, item.Price, item.IP);
                    _productRepo.minusQuantity(item.ProductId, item.Quantity);
                    _productRepo.plusQuantitySold(item.ProductId, item.Quantity);
                    int orderId = _orderItemRepo.getLastOrderId();
                    _revenueRepo.Add(item.OrderId, subTotal);
                    Product product = _db.Products.Find(item.ProductId, item.Quantity);
                    body += _constants.addItem(product.ProductName,item.Quantity, item.Price, subTotal);
                    totalCash += subTotal;
                }
                _customerRepo.minusCustomerWallet(data.First().CustomerId, totalCash);
                body += _constants.addTotalCash(totalCash);
                body = 
                mailRequest =
                    new MailRequest(
                        "arnulfo78@ethereal.email",
                        "Thong tin dat hang",
                        body
                    );
            }
            else
            {
                Console.WriteLine("Không đủ");
                mailRequest = 
                    new MailRequest(
                        "arnulfo78@ethereal.email", 
                        "Thong tin dat hang", 
                        "San pham khong du so luong"
                    );
            }
        }
        else
        {
            mailRequest =
                new MailRequest(
                    "arnulfo78@ethereal.email",
                    "Thong tin dat hang",
                    message.GetProperty("message").GetString()
                );
        }
        _mediator.Send(new SendMailCommand(mailRequest));
        return default;
    }

    //Task IRequestHandler<OrderCommand>.Handle(OrderCommand request, CancellationToken cancellationToken)
    //{
    //    var json = request.consumer.Message.Value;
    //    dynamic message = JsonSerializer.Deserialize<JsonElement>(json);
    //    bool success = message.GetProperty("success").GetBoolean();
    //    if (success)
    //    {
    //        List<OrderItem> data = new List<OrderItem>();
    //        data = JsonSerializer.Deserialize<List<OrderItem>>(message.GetProperty("data"));
    //        bool check = true;
    //        foreach (var item in data)
    //        {
    //            var product = _db.Products.Find(item.ProductId);
    //            if (product != null)
    //            {
    //                check = product.checkQuantity(item.Quantity);
    //            }
    //            else
    //            {
    //                check = false;
    //            }
    //        }
    //        if (check)
    //        {
    //            foreach (var item in data)
    //            {
    //                var product = _db.Products.Find(item.ProductId);
    //                OrderItem orderItem = new OrderItem(item.CustomerId, item.ProductId, item.Quantity, item.Price);
    //                _db.OrderItem.Add(orderItem);
    //                _db.SaveChanges();
    //                product.Quantity -= item.Quantity;
    //                product.QuantitySold += item.Quantity;

    //                OrderItem order = _db.OrderItem.OrderByDescending(r => r.OrderId).LastOrDefault();

    //                Revenue revenue = new Revenue(order.OrderId, item.SubTotal());
    //                _db.Revenue.Add(revenue);

    //                Customer customer = _db.Customers.Find(item.CustomerId);
    //                customer.CustomerWallet -= item.SubTotal();
    //                _db.SaveChanges();
    //            }
    //            Console.WriteLine("OK");
    //        }
    //        else
    //        {
    //            Console.WriteLine("Không đủ");
    //            _mediator.Send(new SendMailCommand("Số lượng sản phẩm không đủ", "hieukhac6869@gmail.com"));
    //        }
    //    }
    //    else
    //    {
    //        _mediator.Send(new SendMailCommand("Thành công", "hieukhac6869@gmail.com"));
    //    }
    //    return Task.CompletedTask;
    //}
}
