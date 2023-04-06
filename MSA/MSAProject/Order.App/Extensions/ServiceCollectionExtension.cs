using Confluent.Kafka;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace Order.App.Extensions;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKafkaConsumer();
        services.AddDbContext(configuration);
        services.AddMediator();
        services.AddSendMail();
        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<DbContextModel>();
        services.AddDbContext<DbContextModel>(options =>
        {
            options.UseOracle(configuration.GetConnectionString("OraDbConnection"));
        });
        return services;
    }
    public static IServiceCollection AddSendMail(this IServiceCollection services)
    {
        services.AddSingleton<SmtpClient>(sp =>
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("musicscendy@gmail.com", "Hieuscendy178@")
            };
            return client;
        });
        return services;
    }
public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
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
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            return new ConsumerBuilder<string, string>(consumerConfig).Build();
        });

        return services;

    }
}
