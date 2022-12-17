using MassTransit;

namespace LcN.Articles.Keda.Consumer;

public class ConsumerDefinition : ConsumerDefinition<Consumer>
{
    public ConsumerDefinition()
    {
        EndpointName = "LcN.Articles.Keda.Consumer";
    }
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
                                              IConsumerConfigurator<Consumer> consumerConfigurator)
    {
        ConcurrentMessageLimit = 1;
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
    }
}