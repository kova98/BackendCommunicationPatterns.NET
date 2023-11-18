public record Item(string Name, double Price);

public class ItemService
{
    private TaskCompletionSource<Item?> _tcs = new();
    private long _id = 0;
    
    public void Reset()
    {
        _tcs = new TaskCompletionSource<Item?>();
    }

    public void NotifyNewItemAvailable()
    {
        _tcs.TrySetResult(new Item($"New Item {_id++}", Random.Shared.Next(0, 500)));
    }

    public Task<Item?> WaitForNewItem()
    {
        // Simulate some delay in Item arrival
        Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(Random.Shared.Next(0, 29)));
            NotifyNewItemAvailable();
        });
        
        return _tcs.Task;
    }
}