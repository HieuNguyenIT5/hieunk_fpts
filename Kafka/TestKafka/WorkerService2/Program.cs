using Confluent.Kafka;
using WorkerService2;

IHost consume = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context,services) =>
    {
        var bootstrapServers = context.Configuration.GetValue<string>("BootstrapServers");
        services.AddSingleton(sp =>
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = "group3",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            return new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        });
        services.AddHostedService<ConsumebackgroundTask>();
    })
    .Build();
await consume.RunAsync();
