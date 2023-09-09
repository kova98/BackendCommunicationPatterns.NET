using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using HTTP.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ItemService>();
// builder.Services.AddControllers();

var app = builder.Build();
// app.MapControllers();

app.MapGet("/items/{id}", async (int id, [FromServices] ItemService itemService) =>
{
    var item = await itemService.GetItem(id);
    return Results.Ok(item);
});
app.MapPost("/items", (Item item) => Results.Ok(item));
app.MapPut("/items/{id}", (int id, Item item) => Results.Ok(item));
app.MapDelete("/items/{id}", (int id) => Results.Ok());
app.MapPatch("/items", (JsonPatchDocument<Item> patchDoc) => Results.Ok());

app.Run();
