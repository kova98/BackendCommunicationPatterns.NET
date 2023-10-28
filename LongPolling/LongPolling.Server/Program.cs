var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ItemService>();
var app = builder.Build();

app.MapGet("/simple", async (CancellationToken userCt, ItemService itemService) =>
{
    var cts = CancellationTokenSource.CreateLinkedTokenSource(userCt);
    cts.CancelAfter(TimeSpan.FromSeconds(30));

    while (!cts.IsCancellationRequested)
    {
        if (itemService.AnyNewItems())
        {
            return Results.Ok(itemService.GetNewItem());
        }
        
        // smaller delay makes server more responsive, but decreases performance
        await Task.Delay(50);
    }

    return Results.NoContent();
});

app.MapGet("/efficient", async (CancellationToken userCt, ItemService itemService) =>
{
    var cts = CancellationTokenSource.CreateLinkedTokenSource(userCt);
    cts.CancelAfter(TimeSpan.FromSeconds(30));
    
    var timeoutTask = Task.Delay(-1, cts.Token);
    var itemArrivedTask = itemService.WaitForNewItem();
    
    var completedTask = await Task.WhenAny(itemArrivedTask, timeoutTask);
    if (completedTask == itemArrivedTask)
    {
        var item = await itemArrivedTask;
        itemService.Reset();
        return Results.Ok(item);
    }

    return Results.NoContent();
});

app.Run();