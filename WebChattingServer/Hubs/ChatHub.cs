using Microsoft.AspNetCore.SignalR;

namespace WebChattingServer.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
    }
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendMessage(string user, string message)
        {
            Console.WriteLine($"{user}: {message}");
            await Clients.All.ReceiveMessage(user, message);
        }
    }
}
