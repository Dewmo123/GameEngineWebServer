using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace WebChattingServer.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"[Connected] {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"[Disconnected] {Context.ConnectionId}");
            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(string message)
        {
            Console.WriteLine($"{Context.User.Identity.Name}:{message}");
            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name,message);
        }
    }
}
