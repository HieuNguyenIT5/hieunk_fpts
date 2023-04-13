using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Hosting;
using Order.App.Application.Command;
using Order.App.Services;
using Order.Domain.AggregateModels;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

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
                    var data               = JsonSerializer.Deserialize<List<OrderItem>>(message.GetProperty("data"));
                    if (data != null)
                    {
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
                        _mediator.Send(new SendMailCommand(mailRequest));
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

