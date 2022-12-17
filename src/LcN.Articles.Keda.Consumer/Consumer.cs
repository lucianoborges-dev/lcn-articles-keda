using LcN.Articles.Keda.Model;
using MassTransit;
using System.Text.Json;

namespace LcN.Articles.Keda.Consumer;

public class Consumer : IConsumer<ISampleData>
{
    readonly ILogger<Consumer> _logger;
    public Consumer(ILogger<Consumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ISampleData> context)
    {
        var json = JsonSerializer.Serialize(context.Message);
        _logger.LogInformation("{Data}", json);

        return Task.CompletedTask;
    }
}
