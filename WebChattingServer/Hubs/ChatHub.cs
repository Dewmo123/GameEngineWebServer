using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace WebChattingServer.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private ConcurrentDictionary<string, string> _nickNames;
        public ChatHub()
        {
            _nickNames = new();
        }
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
        public void SetNickName(string? nickName)
        {
            if (string.IsNullOrEmpty(nickName))
                return;
            _nickNames[Context.ConnectionId] = nickName;
            Console.WriteLine($"[SetNickName] {Context.ConnectionId} : {nickName}");
        }
    }
}
