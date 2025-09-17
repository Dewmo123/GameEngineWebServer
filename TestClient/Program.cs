using BLL.DTOs;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace TestClient
{
    internal class Program
    {
        static string _connection = "https://localhost:7146";

        static async Task Main(string[] args)
        {
            Thread.Sleep(1231);
            var cookieContainer = new CookieContainer();//웹소켓이랑 HTTP랑 쿠키를 안맞춰주면 서로 다른걸로 인식함
            var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            HttpClient client = new(handler);
            string url = $"{_connection}/authorize/sign-up";
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
            //var connection = new HubConnectionBuilder()
            //    .WithUrl($"{_connection}/chat", options =>
            //    {
            //        options.Cookies = cookieContainer;//``
            //    })
            //    .WithAutomaticReconnect()
            //    .Build();
            //Thread.Sleep(1000);
            //var m = await client.GetAsync($"{_connection}/authorize/test");
            //Console.WriteLine(await m.Content.ReadAsStringAsync());
            //connection.On<string, string>("ReceviveMessage", (user, message) =>
            //{
            //    Console.WriteLine($"{user}: {message}");
            //});
            //await connection.StartAsync();
            //while (true)
            //{
            //    var asd = Console.ReadLine();
            //    await connection.SendAsync("SendMessage", asd);
            //}
        }
    }
}
