IHost OrderProcess = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        services.AddConfiguration(configuration);
        services.AddHostedService<OrderBackgroundTask>();
    })
    .Build();
await OrderProcess.RunAsync();
