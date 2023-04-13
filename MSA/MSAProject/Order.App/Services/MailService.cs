using MailKit.Net.Proxy;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Order.App.Settings;
using System.Threading.Tasks;

namespace Order.App.Services;
public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;
    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmail(MailRequest mailRequest)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        email.Subject = mailRequest.Subject;
        var builder = new BodyBuilder();
        builder.HtmlBody = mailRequest.Body;
        email.Body = builder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            if (!string.IsNullOrEmpty(_mailSettings.ProxyHost) && _mailSettings.ProxyPort > 0)
            {
                client.ProxyClient = new Socks5Client(_mailSettings.ProxyHost, _mailSettings.ProxyPort);
            }

            await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls, cancellationToken: default);
            await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password, cancellationToken: default);
            await client.SendAsync(email, cancellationToken: default);
            await client.DisconnectAsync(true, cancellationToken: default);
        }
    }
}
