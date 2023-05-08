using Microsoft.AspNetCore.Http.Internal;

namespace Order.App.Application.Command;
public class OrderCommandHandler : IRequestHandler<OrderCommand>
{
    private readonly IMediator _mediator;
    private readonly IProductRepository _productRepo;
    private readonly IOrderItemRepository _orderItemRepo;
    private readonly IRevenueRepository _revenueRepo;
    private readonly ICustomerRepository _customerRepo;
    private readonly IOrderRepository _orderRepo;
    private readonly INetMQSocket _socket;
    public OrderCommandHandler(
         IMediator mediator,
         IProductRepository productRepo,
         IOrderItemRepository orderItemRepo,
         IRevenueRepository revenueRepo,
         ICustomerRepository customerRepo,
         IOrderRepository orderRepo,
         INetMQSocket socket
     )
    {
        _mediator = mediator;
        _productRepo = productRepo;
        _orderItemRepo = orderItemRepo;
        _revenueRepo = revenueRepo;
        _customerRepo = customerRepo;
        _orderRepo = orderRepo;
        _socket = socket;
    }

    public Task<Unit> Handle(OrderCommand request, CancellationToken cancellationToken)
    {
        bool checkQuantity = true;
        decimal totalCash = 0;
        var mailRequest =
            new MailRequest(
                _customerRepo.FindCustomer(request.Data.CustomerId).CustomerEmail,
                "Thong tin dat hang",
                ""
            );
        Orders order = new Orders(request.Data.CustomerId, request.Data.IP);
        List<OrderItem> items = new List<OrderItem>();
        foreach (var item in request.Data.Items)
        {
            OrderItem itemNew = new OrderItem(item.OrderId, item.ProductId, item.Quantity, item.Price);
            items.Add(itemNew);
            var product = _productRepo.FindProduct(itemNew.ProductId);
            if (!product.checkQuantity(itemNew.Quantity))
            {
                checkQuantity = false;
            }
        }
        if (!checkQuantity)
        {
            var json = new
            {
                success = false,
                message = "Số lượng không đủ"
            };
            _socket.SendFrame(JsonSerializer.Serialize(json));
            mailRequest.Body = "Số lượng hàng trong kho không đủ!";
        }
        else
        {
            order = _orderRepo.addOder(order);
            foreach (var item in items)
            {
                var product = _productRepo.FindProduct(item.ProductId);
                decimal subTotal = item.SubTotal();
                _orderItemRepo.AddOrderItem(order.OrderId, item.ProductId, item.Quantity, item.Price);
                var orderId = _orderItemRepo.GetLastOrderId();
                _productRepo.minusQuantity(item.ProductId, item.Quantity);
                _productRepo.plusQuantitySold(item.ProductId, item.Quantity);
                _customerRepo.minusCustomerWallet(order.CustomerId, subTotal);
                totalCash += item.SubTotal();
                mailRequest.AddItem(product.ProductName, item.Quantity, item.Price, subTotal);
            }
            _revenueRepo.Add(order.OrderId, totalCash);
            var json = new
            {
                success = true,
                message = "Đặt hàng thành công! Vui lòng kiểm tra email."
            };
            _socket.SendFrame(JsonSerializer.Serialize(json));
        }
        mailRequest.Body = Constants.BEGINBODY + mailRequest.Body + Constants.ENDBODY;
        //_mediator.Send(new SendMailCommand(mailRequest));
        return default;
    }
}
