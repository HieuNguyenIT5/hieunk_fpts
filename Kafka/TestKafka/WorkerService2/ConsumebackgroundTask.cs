using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService2
{
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
                _consumer.Subscribe("test-topic1");
                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = _consumer.Consume();
                    _logger.LogInformation(result.Message.Value);
                }
            });
        }
    }
}
