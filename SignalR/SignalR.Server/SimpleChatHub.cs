using Microsoft.AspNetCore.SignalR;

public class SimpleChatHub : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}