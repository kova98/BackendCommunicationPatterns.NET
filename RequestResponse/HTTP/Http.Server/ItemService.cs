namespace HTTP.Server;

public class ItemService
{
    public async Task<Item> GetItem(int id)
    {
        return await Task.FromResult(new Item());
    }
}