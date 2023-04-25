namespace Order.App.Application.Command;
public class SendMailCommandHandler : IRequestHandler<SendMailCommand>
{
    private readonly IMailService mailService;
    public SendMailCommandHandler(IMailService mailService)
    {
        this.mailService = mailService;
    }

    public async Task<Unit> Handle(SendMailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await mailService.SendEmail(request.MailRequest);
            return Unit.Value;
        }
        catch (Exception ex)
        {
            // Xử lý exception ở đây nếu cần
            throw;
        }
    }
}
