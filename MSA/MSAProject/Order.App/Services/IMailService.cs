namespace Order.App.Services;

public interface IMailService
{
    Task SendEmail(MailRequest mailRequest);
}
