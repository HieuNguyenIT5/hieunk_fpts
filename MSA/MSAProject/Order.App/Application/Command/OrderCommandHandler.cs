using MediatR;
using Order.App.Services;
using Order.Domain.AggregateModels;
using Order.Infrastructure;
using Order.Infrastructure.Repositories;
using Order.App.Common;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Order.App.Application.Command;
public class OrderCommandHandler : IRequestHandler<OrderCommand>
{
    private readonly DbContextModel _db;
    private readonly IMediator _mediator;
    private IProductRepository _productRepo;
    private IOrderItemRepository _orderItemRepo;
    private IRevenueRepository _revenueRepo;
    private ICustomerRepository _customerRepo;
    public OrderCommandHandler(
         DbContextModel db,
         IMediator mediator,
         IProductRepository productRepo,
         IOrderItemRepository orderItemRepo,
         IRevenueRepository revenueRepo,
         ICustomerRepository customerRepo
     )
    {
        _db = db;
        _mediator = mediator;
        _productRepo = productRepo;
        _orderItemRepo = orderItemRepo;
        _revenueRepo = revenueRepo;
        _customerRepo = customerRepo;
    }

    public Task<Unit> Handle(OrderCommand request, CancellationToken cancellationToken)
    {
        bool checkQuantity = true;
        decimal totalCash = 0;
        string body = "";
        bool success = true;
        foreach (var item in request.Data)
        {
            var product = _db.Products.Find(item.ProductId);
            checkQuantity = product != null ? product.checkQuantity(item.Quantity) : false;
            if (checkQuantity)
            {
                var subTotal = item.SubTotal();
                var orderId = _orderItemRepo.GetLastOrderId();
                _orderItemRepo.AddOrderItem(item.CustomerId, item.ProductId, item.Quantity, item.Price, item.IP);
                _productRepo.minusQuantity(item.ProductId, item.Quantity);
                _productRepo.plusQuantitySold(item.ProductId, item.Quantity);
                _revenueRepo.Add(orderId, subTotal);
                totalCash += subTotal;
                body += AddItem(product.ProductName, item.Quantity, item.Price, subTotal);
            }
            else
            {
                body = "San pham khong du so luong";
                success = false;
                break;
            }
        }
        if (success)
        {
            body = Constants.BEGINBODY + body + Constants.ENDBODY;
        }
        var mailRequest =
            new MailRequest(
                "arnulfo78@ethereal.email",
                "Thong tin dat hang",
                body
            );
        _mediator.Send(new SendMailCommand(mailRequest));
        return default;
    }
    public string AddItem(string ProductName, int Quantity, decimal Price, decimal SubTotal)
    {
        return "<tr>" +
               "<td>" + ProductName + "</td>" +
               "<td>" + Quantity + "</td>" +
               "<td>" + Price + "</td>" +
               "<td>" + SubTotal + "</td>" +
               "</tr>";
    }
    public string AddTotalCash(decimal totalCash)
    {
        return "<tr>" +
               "<td colspan='3'>Tổng tiền:</td>" +
               "<td>" + totalCash + "</td>" +
               "</tr>";
    }
}
