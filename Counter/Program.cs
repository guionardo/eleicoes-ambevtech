using Counter;
using SharedResources.Configuracao;
using SharedResources.Repositories;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
        .AddTransient<Configuracao>()
        .AddTransient<IBrokerReceiver, BrokerReceiver>()
        .AddTransient<ICounterService, CounterService>()
        .AddHostedService<Worker>()
        .AddTransient<IElectionRepository, ElectionRepository>();
    })
    .Build();

host.Run();
