using Order.App.Services;
using MediatR;
namespace Order.App.Application.Command;
public class SendMailCommandHandler : IRequestHandler<SendMailCommand>
{
    private readonly IMailService mailService;
    public SendMailCommandHandler(IMailService mailService)
    {
        this.mailService = mailService;
    }

    Task<Unit> IRequestHandler<SendMailCommand, Unit>.Handle(SendMailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            mailService.SendEmail(request.MailRequest);
            return default;
        }
        catch (Exception ex)
        {
            throw;
        }
        return default;
    }
}
