using MediatR;

namespace Order.App.Application.Command;
public class SendMailCommand : IRequest
{
    public string message { get; set; }
    public string  email{ get; set; }
    public SendMailCommand(string message, string email)
    {
        this.email = email;
        this.message = message;
    }
}
