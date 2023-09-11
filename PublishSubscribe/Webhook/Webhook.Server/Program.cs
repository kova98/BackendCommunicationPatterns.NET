using Webhook.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<WebhookService>();

var app = builder.Build();

app.MapPost("/subscribe", (WebhookService ws, Subscription sub) 
    => ws.Subscribe(sub));

app.MapPost("/publish", async (WebhookService ws, PublishRequest req) 
    => await ws.PublishMessage(req.Topic, req.Message));

app.Run();

record PublishRequest(string Topic, object Message);

