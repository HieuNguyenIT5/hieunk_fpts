namespace Order.App.Application.Command;
public class OrderCommandHandler : IRequestHandler<OrderCommand>
{
    private readonly IMediator _mediator;
    private readonly IProductRepository _productRepo;
    private readonly IOrderItemRepository _orderItemRepo;
    private readonly IRevenueRepository _revenueRepo;
    private readonly ICustomerRepository _customerRepo;
    private readonly INetMQSocket _socket;
    public OrderCommandHandler(
         IMediator mediator,
         IProductRepository productRepo,
         IOrderItemRepository orderItemRepo,
         IRevenueRepository revenueRepo,
         ICustomerRepository customerRepo,
         INetMQSocket socket
     )
    {
        _mediator = mediator;
        _productRepo = productRepo;
        _orderItemRepo = orderItemRepo;
        _revenueRepo = revenueRepo;
        _customerRepo = customerRepo;
        _socket = socket;
    }

    public Task<Unit> Handle(OrderCommand request, CancellationToken cancellationToken)
    {
        bool checkQuantity = true;
        decimal totalCash = 0;
        var mailRequest =
            new MailRequest(
                "arnulfo78@ethereal.email",
                "Thong tin dat hang",
                ""
            );
        foreach (var item in request.Data)
        {
            var product = _productRepo.FindProduct(item.ProductId);
            if (!product.checkQuantity(item.Quantity))
            {
                checkQuantity = false;
            }
            totalCash += item.SubTotal();
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
            foreach (var item in request.Data)
            {
                var product = _productRepo.FindProduct(item.ProductId);
                decimal subTotal = item.SubTotal();
                _orderItemRepo.AddOrderItem(item.CustomerId, item.ProductId, item.Quantity, item.Price, item.IP, 1);
                var orderId = _orderItemRepo.GetLastOrderId();
                _productRepo.minusQuantity(item.ProductId, item.Quantity);
                _productRepo.plusQuantitySold(item.ProductId, item.Quantity);
                _customerRepo.minusCustomerWallet(item.CustomerId, subTotal);
                _revenueRepo.Add(orderId, subTotal);
                mailRequest.AddItem(product.ProductName, item.Quantity, item.Price, subTotal);
            }
            var json = new
            {
                success = true,
                message = "Đặt hàng thành công! Vui lòng kiểm tra email."
            };
            _socket.SendFrame(JsonSerializer.Serialize(json));
        }
        mailRequest.Body = Constants.BEGINBODY + mailRequest.Body + Constants.ENDBODY;
        _mediator.Send(new SendMailCommand(mailRequest));
        return default;
    }
}
