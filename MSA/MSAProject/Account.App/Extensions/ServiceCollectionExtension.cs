﻿using Account.Domain.AggregateModels;
using Account.Infrastructure;
using Account.Infrastructure.Repositories;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Reflection;

namespace Account.App.Extensions;
public static class ServiceCollectionExtension
{

    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContext(configuration);
        services.AddKafkaConsumer(configuration);
        services.AddKafkaProducer(configuration);
        services.AddMediator();
        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddDbContext<DbContextModel>(options =>
        {
            options.UseOracle(configuration.GetConnectionString("OraDbConnection"));
        });
        return services;
    }
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
    public static KafkaSettings getKafkaString(IConfiguration configuration)
    {
        var kafkaSettings = configuration.GetSection("KafkaSetting").Get<KafkaSettings>();

        if (kafkaSettings is null)
        {
            return new KafkaSettings();
        }
        return kafkaSettings;
    }
    public static IServiceCollection AddKafkaProducer(this IServiceCollection services, IConfiguration configuration)
    {
        KafkaSettings kafkaSettings = getKafkaString(configuration);
        services.AddSingleton(sp =>
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = kafkaSettings.HostPort,
            };
            return new ProducerBuilder<string, string>(producerConfig).Build();
        });
        return services;
    }
    public static IServiceCollection AddKafkaConsumer(this IServiceCollection services, IConfiguration configuration)
    {
        KafkaSettings kafkaSettings = getKafkaString(configuration);
        services.AddSingleton(sp =>
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = kafkaSettings.HostPort,
                GroupId = kafkaSettings.Group,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true,
            };

            return new ConsumerBuilder<string, string>(consumerConfig).Build();
        });
        return services;
    }
}
