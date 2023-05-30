namespace Counter
{
    public interface ICounterService
    {
        Task StartListeningAsync(CancellationToken stoppingToken);
    }
}
