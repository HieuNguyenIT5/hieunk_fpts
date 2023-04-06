using MailKit;
using MediatR;
using MimeKit.Text;
using MimeKit;
using System.Net;
using System.Net.Mail;

namespace Order.App.Application.Command
{
    
    public class SendMailCommandHandler : IRequestHandler<SendMailCommand>
    {
        private readonly IMailTransport _transport;
        public SendMailCommandHandler(IMailTransport transport) 
        { 
            _transport = transport;
        }
        public Task Handle(SendMailCommand request, CancellationToken cancellationToken)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("musicscendy@gmail.com"));
            email.To.Add(MailboxAddress.Parse("hieukhac6869@gmail.com"));
            email.Subject = "Test Email Subject";
            email.Body = new TextPart(TextFormat.Plain) { Text = "Example Plain Text Message Body" };
            _transport.Send(email);
            return Task.CompletedTask;
        }
    }
}
