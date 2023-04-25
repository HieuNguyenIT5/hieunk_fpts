namespace Order.App.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKafkaConsumer(configuration);
        services.AddDbContext(configuration);
        services.AddMediator();
        services.AddSendMail(configuration);
        services.AddPubZeroMQ(configuration);
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

    public static IServiceCollection AddPubZeroMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<INetMQSocket, NetMQSocket>(sp =>
        {
            var publisher = new PublisherSocket();
            publisher.Bind(configuration.GetConnectionString("ZeroMQConnection").ToString());
            return publisher;
        });
        return services;
    }
}
