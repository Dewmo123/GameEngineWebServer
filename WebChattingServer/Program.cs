using Microsoft.AspNetCore.ResponseCompression;
using WebChattingServer.Hubs;

namespace WebChattingServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSignalR();
            builder.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    ["application/octet-stream"]);
            });
            var app = builder.Build();
            app.MapGet("/", () => "Hello World!");
            app.MapHub<ChatHub>("/chat");
            app.Run();
        }
    }
}
