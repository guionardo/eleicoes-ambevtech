using Counter;
using SharedResources.Repositories;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
        .AddHostedService<Worker>()
        .AddScoped<IElectionRepository, ElectionRepository>();
    })
    .Build();

host.Run();
