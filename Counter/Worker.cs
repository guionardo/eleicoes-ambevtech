namespace Counter
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICounterService _counterService;

        public Worker(ILogger<Worker> logger, ICounterService counterService)
        {
            _logger = logger;
            _counterService = counterService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _counterService.StartListeningAsync(stoppingToken);
        }
    }
}