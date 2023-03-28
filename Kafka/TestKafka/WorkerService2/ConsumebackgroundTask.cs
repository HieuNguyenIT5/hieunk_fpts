using Confluent.Kafka;

namespace WorkerService2;
public class ConsumebackgroundTask : BackgroundService
{
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly ILogger<ConsumebackgroundTask> _logger;

    public ConsumebackgroundTask(IConsumer<Ignore, string> consumer, ILogger<ConsumebackgroundTask> logger) 
    {
        _consumer = consumer;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(() =>
        {
            Console.WriteLine("Nhap ten topic: ");
            var topic = Console.ReadLine();
            _consumer.Subscribe(topic);
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = _consumer.Consume();
                _logger.LogInformation(result.Message.Value);
            }
        });
    }
}
