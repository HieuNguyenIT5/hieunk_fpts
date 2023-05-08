using Order.Domain.DTOs;

namespace Order.App.BackgroundTasks;
public class OrderBackgroundTask : BackgroundService
{
    //inject consumer
    private readonly IConsumer<string, string> _consumer;
    private readonly IMediator _mediator;
    private readonly INetMQSocket _socket;
    public OrderBackgroundTask(IConsumer<string, string> consumer, IMediator mediator, INetMQSocket socket)
    {
        _consumer = consumer;
        _mediator = mediator;
        _socket = socket;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Run(() =>
        {
            ProcessQueue(stoppingToken);
        });
        return Task.CompletedTask;
    }

    private void ProcessQueue(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("order");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                Task.Run(() =>
                {
                    string messageComsumer = consumeResult.Message.Value;
                    dynamic message        = JsonSerializer.Deserialize<JsonElement>(messageComsumer);
                    bool success           = message.GetProperty("success").GetBoolean();

                    if (success)
                    {
                        var data = JsonSerializer.Deserialize<OrderDto>(message.GetProperty("data"));
                        _mediator.Send(new OrderCommand(data));
                
                    }
                    else
                    {
                        var mailRequest =
                        new MailRequest(
                            "arnulfo78@ethereal.email",
                            "Thong tin dat hang",
                            message.GetProperty("message").GetString()
                        );

                        _socket.SendFrame(messageComsumer);
                        //_mediator.Send(new SendMailCommand(mailRequest));
                    }
                });
            }
            catch (ConsumeException ex)
            {
                // log
                Console.WriteLine(ex.Message);
            }
        }
    }
}

