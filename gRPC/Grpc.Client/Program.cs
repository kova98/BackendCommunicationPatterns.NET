using Grpc.Client;
using Grpc.Net.Client;
using Grpc.Server;
using Microsoft.Extensions.DependencyInjection;

var channel = GrpcChannel.ForAddress("http://localhost:5143");
var client = new Greeter.GreeterClient(channel);

var reply = client.SayHello(new HelloRequest { Name = "World" });
Console.WriteLine("Greeting: " + reply.Message);

// Using the Client factory
var services = new ServiceCollection();
services.AddTransient<GreeterService>();
services.AddGrpcClient<Greeter.GreeterClient>(o =>
{
    o.Address = new Uri("http://localhost:5143");
});

var serviceProvider = services.BuildServiceProvider();
var greeterService = serviceProvider.GetRequiredService<GreeterService>();
var serviceGetResponse = await greeterService.SayHelloAsync("World");

Console.WriteLine(serviceGetResponse);