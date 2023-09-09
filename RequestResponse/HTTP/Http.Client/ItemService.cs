using System.Net.Http.Json;

namespace RequestResponse.Console;

public class ItemService
{
    private readonly HttpClient _client;

    public ItemService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("Local");
    }

    public async Task<Item?> GetItem(int id)
    {
        return await _client.GetFromJsonAsync<Item>($"items/{id}");
    }
}