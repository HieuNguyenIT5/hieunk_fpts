namespace Order.App.Application.Command;
public class SendMailCommand : IRequest
{
    public MailRequest MailRequest{ get; set; }

    public SendMailCommand(MailRequest mailRequest)
    {
        this.MailRequest = mailRequest;
    }
}
