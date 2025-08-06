using BLL.DTOs;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Text;

namespace TestClient
{
    internal class Program
    {
        static string _connection = "https://localhost:7146";

        static async Task Main(string[] args)
        {
            Thread.Sleep(1231);
            HttpClient client = new();
            string url = $"{_connection}/authorize/login";
            LoginDTO dto = new()
            {
                Password = "smmania2874",
                UserId = "dewmo"
            };
            string json = JsonConvert.SerializeObject(dto);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage msg = await client.PostAsync(url, content);
            string response = await msg.Content.ReadAsStringAsync();
            Console.WriteLine(response);
            var connection = new HubConnectionBuilder()
                .WithUrl($"{_connection}/chat")
                .WithAutomaticReconnect()
                .Build();
            connection.On<string, string>("ReceviveMessage", (user, message) =>
            {
                Console.WriteLine($"{user}: {message}");
            });
            await connection.StartAsync();
            while (true)
            {
                var asd = Console.ReadLine();
                await connection.SendAsync("SendMessage", asd);
            }
        }
    }
}
