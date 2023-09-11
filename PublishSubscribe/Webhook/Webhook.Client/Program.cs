const string server = "http://localhost:5003";
const string callback = "http://localhost:5004/wh/item/created";
const string topic = "item.created";

var client = new HttpClient();

Console.WriteLine($"Subscribing to topic {topic} with callback {callback}");
await client.PostAsJsonAsync(server + "/subscribe", new { topic, callback });

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();

var app = builder.Build();
app.MapPost("/wh/item/created", (object payload, ILogger<Program> logger) =>
{
    logger.LogInformation("Received payload: {payload}", payload);
});
app.Run();