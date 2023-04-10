using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Hosting;
using Order.App.Application.Command;

namespace Order.App.BackgroundTasks;
public class OrderBackgroundTask : BackgroundService
{
    //inject consumer
    private readonly IConsumer<string, string> _consumer;
    private readonly IMediator _mediator;
    public OrderBackgroundTask(IConsumer<string, string> consumer, IMediator mediator)
    {
        _consumer = consumer;
        _mediator = mediator;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("order");
        ConsumeResult<string, string> result = _consumer.Consume();
        _mediator.Send(new OrderCommand(result));
        return Task.CompletedTask;
    }
}
