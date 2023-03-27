using Confluent.Kafka;
using WorkerService1;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var bootstrapServers = context.Configuration.GetValue<string>("BootstrapServers");
        services.AddSingleton(sp =>
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            };
            return new ProducerBuilder<Null, string>(producerConfig).Build();
        });
        services.AddSingleton<Main>();
    })
    .Build();
var main = host.Services.GetRequiredService<Main>();
await main.RunAsync();
