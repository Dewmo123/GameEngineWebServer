using Microsoft.AspNetCore.SignalR;

namespace WebChattingServer.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
    }
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string? user, string? message)
        {
            Console.WriteLine($"{user}: {message}");
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task Echo()
        {
            Console.WriteLine($"[Echo]: ");
            await Clients.Caller.SendAsync("ReceiveMessage", "Server", "ASDDS");
        }
    }
}
