using Confluent.Kafka;

namespace Account.App.Extensions;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddConfiguration(this IServiceCollection services)
    {
        services.AddKafkaProducer();
        return services;
    }

    public static IServiceCollection AddKafkaProducer(this IServiceCollection services)
    {
        services.AddSingleton(sp =>
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };
            return new ProducerBuilder<string, string>(producerConfig).Build();
        });

        return services;

    }
}


