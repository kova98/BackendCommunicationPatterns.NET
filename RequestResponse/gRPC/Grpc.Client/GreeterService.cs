using Grpc.Server;

namespace Grpc.Client;

public class GreeterService
{
    private readonly Greeter.GreeterClient _client;

    public GreeterService(Greeter.GreeterClient client)
    {
        _client = client;
    }

    public async Task<string> SayHelloAsync(string name)
    {
        var reply = await _client.SayHelloAsync(new HelloRequest { Name = name });
        return reply.Message;
    }
}