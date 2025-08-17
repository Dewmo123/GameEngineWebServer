using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using WebChattingServer.Rooms;

namespace WebChattingServer.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private RoomManager _roomManager;
        public ChatHub(RoomManager roomManager)
        {
            _roomManager = roomManager;
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
        public async Task SendMessage(string message)
        {
            Console.WriteLine($"{Context.User.Identity.Name}:{message}");
            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
        }
        public async Task EnterRoom(int roomId)
        {
            if (_roomManager.CheckCanEnter(roomId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
            }
        }
    }
}
