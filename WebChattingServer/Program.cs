using BLL.Services;
using DAL.Repositories;
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
            builder.Services.AddAuthentication();
            builder.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    ["application/octet-stream"]);
            });
            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connection))
                throw new NullReferenceException();
            builder.Services.AddTransient(provider=>new AuthorizeRepository(connection));
            
            var app = builder.Build();
            app.UseRouting(); // Ãß°¡
            app.UseAuthorization();
            app.MapGet("/", () => "Hello World!");
            app.MapHub<ChatHub>("/chat");
            app.Run();
        }
    }
}
