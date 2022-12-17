using MassTransit;

namespace LcN.Articles.Keda.Producer;

public class ProducerFactory : BackgroundService
{
    private readonly IBus _bus;
    private readonly ILogger<Producer> _logger;
    public ProducerFactory(IBus bus, ILogger<Producer> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var tasks = new List<Task>();

        for (ulong i = 0; i < 50; i++)
        {
            var producer = new Producer(i, _bus, _logger, stoppingToken);
            tasks.Add(producer.RunAsync());

            if (stoppingToken.IsCancellationRequested)
            {
                break;
            }

            await Task.Delay(5000, stoppingToken);
        }

        await Task.WhenAll(tasks.ToArray());
    }
}