using Confluent.Kafka;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.App.Application.Command;
using Order.App.Services;
using Order.App.Settings;
using Order.Domain.AggregateModels;
using Order.Infrastructure;
using Order.Infrastructure.Repositories;
using System;

namespace Order.App.Extensions;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKafkaConsumer(configuration);
        services.AddDbContext(configuration);
        services.AddMediator();
        services.AddSendMail(configuration);
        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddSingleton<DbContextModel>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IRevenueRepository, RevenueRepository>();
        services.AddDbContext<DbContextModel>(options =>
        {
            options.UseOracle(configuration.GetConnectionString("OraDbConnection"));
        });
        return services;
    }
    public static IServiceCollection AddSendMail(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddTransient<IMailService, Services.MailService>();
        return services;
    }
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(typeof(OrderCommand));
        return services;
    }
    public static IServiceCollection AddKafkaConsumer(this IServiceCollection services, IConfiguration configuration)
    {
        var kafkaSettings = configuration.GetSection("KafkaSetting").Get<KafkaSettings>();

        if (kafkaSettings is null)
        {
            throw new ArgumentNullException(nameof(kafkaSettings), "Kafka settings are missing or invalid.");
        }

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
