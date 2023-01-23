using LcN.Articles.Keda.Model;
using MassTransit;
using System.Collections.Concurrent;

namespace LcN.Articles.Keda.Api.Services;

public class ProducerService
{
    private readonly ConcurrentDictionary<Guid, ProducerData> _producers = new();
    private readonly ILogger<ProducerService> _logger;
    private readonly IBus _bus;

    public ProducerService(IBus bus, ILogger<ProducerService> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public ProducerData StartProducer(string name, int numOfItems)
    {
        var producerData = new ProducerData(name, numOfItems);

        _logger.LogInformation("Starting a new producer: Guid: {Guid}, Name: {Name}, NumOfItems: {NumOfItems}", producerData.Id, producerData.Name, producerData.NumOfItems);

        var task = new Task(async () =>
        {
            await Produce(producerData);
        });

        task.Start();

        return _producers.AddOrUpdate(producerData.Id, id => producerData, (id, prod) => prod);
    }

    public bool StopProducer(Guid guid)
    {
        if (_producers.TryRemove(guid, out var producer))
        {
            producer.TokenSource.Cancel();
            return true;
        }

        return false;
    }

    public IEnumerable<ProducerData> GetProducers()
    {
        foreach (var (_, producer) in _producers)
        {
            yield return producer;
        }
    }

    private async Task Produce(ProducerData producerData)
    {
        var cancellationToken = producerData.TokenSource.Token;

        for (int i = 0; i < producerData.NumOfItems; i++)
        {
            var item = $"{DateTime.Now:yyyyMMdd-HHmmss-fff}-{i:x8}";
            _logger.LogInformation("[{Id}] Producer {Name} is publishing an item: {item}", producerData.Id, producerData.Name, item);

            await _bus.Publish<IConsumerData>(new
            {
                producerData.Id,
                producerData.Name,
                Data = item
            }, cancellationToken);

            await Task.Delay(1000);

            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }
        }
    }
}
