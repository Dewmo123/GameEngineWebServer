using Microsoft.AspNetCore.ResponseCompression;
using WebChattingServer.Hubs;

namespace WebChattingServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddSignalR();
            var app = builder.Build();

            app.MapHub<ChatHub>("/chat");

            app.Run();
        }
    }
}
