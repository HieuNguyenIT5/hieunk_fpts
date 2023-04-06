using System.Runtime.CompilerServices;
using Confluent.Kafka;
namespace Order.App.Extensions;

public static class ServiceCollectionExtention
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services)
    {
        services.AddKafkaConsumer();
        return services;
    }

    public static IServiceCollection AddKafkaConsumer(this IServiceCollection services)
    {
        services.AddSingleton(sp =>
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "group1",
                AutoOffsetReset = AutoOffsetReset.Latest
            };
            return new ConsumerBuilder<string, string>(consumerConfig).Build();
        });

        return services;

    }
}
