using LcN.Articles.Keda.Producer;
using MassTransit;

await Host.CreateDefaultBuilder(args)
.ConfigureServices(services =>
{
    services.AddHostedService<ProducerFactory>();
    services.AddMassTransit(x =>
    {
        x.SetKebabCaseEndpointNameFormatter();
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host("localhost", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
        });
    });
})
.RunConsoleAsync();