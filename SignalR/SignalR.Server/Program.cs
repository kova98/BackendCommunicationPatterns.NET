using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAnyOrigin", p => p
        .WithOrigins("null") // Origin of an html file opened in a browser
        .AllowAnyHeader()
        .AllowCredentials()); 
});
builder.Services.AddSignalR();

var app = builder.Build();
app.UseCors("AllowAnyOrigin");

app.MapHub<ChatHub>("/chatHub");
app.Run();

public class ChatHub : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}