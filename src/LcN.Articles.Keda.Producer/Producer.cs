using LcN.Articles.Keda.Model;
using MassTransit;

namespace LcN.Articles.Keda.Producer;

public class Producer
{
    private ulong _item = 1;

    private readonly IBus _bus;
    private readonly CancellationToken _cancellationToken;
    private readonly ILogger<Producer> _logger;
    private readonly ulong _producerID;

    public Producer(ulong producerID, IBus bus, ILogger<Producer> logger, CancellationToken cancellationToken)
    {
        _bus = bus;
        _cancellationToken = cancellationToken;
        _logger = logger;
        _producerID = producerID;
    }

    public async Task RunAsync()
    {
        for (int i = 0; i < 5000; i++)
        {
            var value = $"Item: {_producerID * _item++}";

            _logger.LogInformation("Producer {producer} : {value}", _producerID, value);

            await _bus.Publish<ISampleData>(new
            {
                Id = _producerID,
                Value = value,
            }, _cancellationToken);

            await Task.Delay(1000, _cancellationToken);

            if (_cancellationToken.IsCancellationRequested)
            {
                break;
            }
        }
    }
}
