using Confluent.Kafka;

namespace Order.App.BackgroundTasks
{
    public class OrderBackgroundTask : BackgroundService
    {
        //inject consumer
        private readonly IConsumer<string, string> _consumer;
        public OrderBackgroundTask( IConsumer<string, string> consumer)
        {
            _consumer = consumer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //_consumer.Con
            _consumer.Subscribe("order");
            var result = _consumer.Consume();
            return Task.FromResult(result);
        }
    }
}
