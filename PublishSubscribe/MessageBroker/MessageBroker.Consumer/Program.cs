using MassTransit;
using MessageBroker.Consumer;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// Console.Write("Enter queue name: ");
// var queue = Console.ReadLine();

services.AddMassTransit(x =>
{
    x.AddConsumer<ItemCreatedMessageConsumer>();
    
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        
        cfg.ConfigureEndpoints(context);
        // cfg.ReceiveEndpoint(queue, e =>
        // {
        //     e.Consumer<ItemCreatedMessageConsumer>();
        // });
    });
});

var serviceProvider = services.BuildServiceProvider();
var bus = serviceProvider.GetRequiredService<IBusControl>();
await bus.StartAsync();

Console.WriteLine("Waiting for messages...");
Console.ReadLine();