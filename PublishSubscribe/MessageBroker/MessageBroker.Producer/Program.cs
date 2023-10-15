using MassTransit;
using MessageBroker.Contracts;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    });
});

var serviceProvider = services.BuildServiceProvider();
var bus = serviceProvider.GetRequiredService<IBusControl>();
await bus.StartAsync(); 

Console.WriteLine("Press any key to send a message...");
while (true)
{
    Console.ReadKey(true);
    await bus.Publish(new ItemCreatedMessage(Name: "Bucket", Price: 12.55));
    Console.WriteLine("Message sent.");
}