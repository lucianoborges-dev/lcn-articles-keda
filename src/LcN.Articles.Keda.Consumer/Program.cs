using MassTransit;
using System.Reflection;

await Host.CreateDefaultBuilder(args)
.ConfigureServices((hostContext, services) =>
{
    services.AddMassTransit(x =>
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        var host = hostContext.Configuration["RabbitMQ"];

        x.AddConsumers(entryAssembly);
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(host, "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
            cfg.ConfigureEndpoints(context);
        });
    });
})
.Build()
.RunAsync();