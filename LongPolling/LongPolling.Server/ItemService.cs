public class ItemService
{
    public bool AnyNewItems() => Random.Shared.Next(0, 100) == 1;
    public string GetNewItem() => "New item";
    
    private TaskCompletionSource<string?> _tcs = new();
    
    public void Reset()
    {
        _tcs = new TaskCompletionSource<string?>();
    }

    public void NotifyNewItemAvailable()
    {
        _tcs.TrySetResult("New Item");
    }

    public Task<string?> WaitForNewItem()
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