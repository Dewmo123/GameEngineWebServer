using Microsoft.AspNetCore.SignalR;

namespace WebChattingServer.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"[Connected] {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string? user, string? message)
        {
            Console.WriteLine($"[SendMessage] {user}: {message}");
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
