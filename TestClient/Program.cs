using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace TestClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7146/chat")
                .WithAutomaticReconnect()
                .Build();
            connection.On<string,string>("ReceiveMessage", HandleChat);
            await connection.StartAsync();
            while (true)
            {
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    continue;
                await connection.SendAsync("SendMessage", (string)"Dewmo", (string)input);
            }
        }

        private static void HandleChat(string user,string msg)
        {
            Console.WriteLine($"{user}: {msg}");
        }
    }
}
