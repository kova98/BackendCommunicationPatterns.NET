using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using RequestResponse.Console;

var client = new HttpClient { BaseAddress = new Uri("http://localhost:5001/") };
var item = new Item(5, "Item 5");

var postResponse = await client.PostAsJsonAsync("items", item);
var putResponse = await client.PutAsJsonAsync("/items/5", item);
var patchResponse = await client.PatchAsJsonAsync("/items", new {});
var getResponse = await client.GetFromJsonAsync<Item>("items/5");
var deleteResponse = await client.DeleteAsync("/items/5");

Console.WriteLine(postResponse);
Console.WriteLine(putResponse);
Console.WriteLine(patchResponse);
Console.WriteLine(getResponse);
Console.WriteLine(deleteResponse);

// Using the IHttpClientFactory
var services = new ServiceCollection();
services.AddHttpClient("Local", client =>
{
    client.BaseAddress = new Uri("http://localhost:5001/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(15);
});
services.AddTransient<ItemService>();

var serviceProvider = services.BuildServiceProvider();
var itemService = serviceProvider.GetRequiredService<ItemService>();
var serviceGetResponse = await itemService.GetItem(5);

Console.WriteLine(serviceGetResponse);