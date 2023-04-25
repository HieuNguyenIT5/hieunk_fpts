namespace Account.App.Application.DomainEventHandlers;
public class CheckCashCustomerDomainEventHandler : IRequestHandler<CheckCashCustomerDomainEvent,string>
{
    private readonly ICustomerRepository _custommerRepo;
    public CheckCashCustomerDomainEventHandler(
        ICustomerRepository custommerRepo
    )
    {
        _custommerRepo = custommerRepo;
    }

    public async Task<string> Handle(CheckCashCustomerDomainEvent request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
           return string.Empty;
        }
        var customer = _custommerRepo.FindCustomer(request.orders[0].CustomerId);
        if (customer == null)
        {
            return JsonSerializer
                .Serialize(new {
                    success = false,
                    message = "Người dùng không tồn tại!"
                });
        }
        else if(customer.CustomerWallet > request.orderTotal)
        {
            return JsonSerializer.Serialize(new { success = true, data = request.orders});
        }
        return JsonSerializer
                .Serialize(new
                {
                    success = false,
                    message = "Số tiền trong tài khoản không đủ để thực hiện giao dịch!"
                });
    }
}
