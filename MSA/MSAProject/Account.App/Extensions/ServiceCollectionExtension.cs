using Account.Infrastructure;
using System.Reflection;

namespace Account.App.Extensions;
public static class ServiceCollectionExtension
{

    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddRepositories();
        services.AddQueries();
        services.AddDbContextOrcl(configuration);
        services.AddKafkaProducer(configuration);
        services.AddMediator();
        services.AddSubZeroMQ(configuration);
        services.AddHttpContextAccessor();
        return services;
    }
    public static IServiceCollection AddDbContextOrcl(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DbContextModel>(options =>
        {
            options.UseOracle(configuration.GetConnectionString("OraDbConnection"), b => b.MigrationsAssembly("Account.App"));
        });
        return services;
    }
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddScoped<ICustomerQueries, CustomerQueries>();
        services.AddScoped<IOrderQueries, OrderQueries>();
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
    public static IServiceCollection AddSubZeroMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<INetMQSocket, NetMQSocket>(sp =>
        {
            var subscriber = new SubscriberSocket();
            subscriber.Connect(configuration.GetConnectionString("ZeroMQConnection").ToString());
            subscriber.Subscribe("");
            return subscriber;
        });
        return services;
    }
}