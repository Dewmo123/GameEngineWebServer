using BLL.Caching;
using BLL.DTOs;
using BLL.Services.Authorizes;
using BLL.Services.Players;
using DAL.VOs;
using WebChattingServer.Hubs;

namespace WebChattingServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSignalR();
            SetAuthorizeAndAuthentification(builder);
            builder.Services.AddAutoMapper(config =>
            {
                config.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg1MjgzMjAwIiwiaWF0IjoiMTc1Mzc4NDM2NSIsImFjY291bnRfaWQiOiIwMTk4NTViMGZiZjg3YTY4YWQwMDQ2YTZjY2ZlYTdhZCIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazFhdjNjOXo2MzZtanNjcHFkMHA3NDFiIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.OHCOAU0UzHNQ2E-_8VDmqujSwP-cRGthlX4jpIe83jZn4UxWykkz0rEub9tL6WhORYuPICOcAatGNZN7BV5EKNndhUsw2BcaeIvNsjmG7HUnjJTFdUjvq-_RubyMw1xVVvaAGwXwaUGSAzOtoSGm2co-2-ApKBfeVVUNVxTdPMzpf4is8SyCmZltoEFWuIGV5V8UpfxqOvkt5qWb4clMXtieIbyAMFpoeaM5wqmMHT1wJEw3v36y34ptEUWTorYt6KKawngXf7B6Z9NzNYpxI3WXYpqE976TAhjeaxUHP8DCV4daHTzjmUcqXq7Gq6i8Wis6V9uN5Vthq0Pe2Lp3mg";
                config.CreateMap<LoginVO, LoginUserDTO>();
                config.CreateMap<LoginUserDTO, LoginVO>();
                config.CreateMap<StatDTO, StatVO>();
                config.CreateMap<StatVO, StatDTO>();
                config.CreateMap<SkillVO, SkillDTO>();
                config.CreateMap<SkillDTO, SkillVO>();
                config.CreateMap<ChapterDTO, ChapterVO>();
                config.CreateMap<ChapterVO, ChapterDTO>();
                config.CreateMap<PartnerVO, PartnerDTO>();
                config.CreateMap<PartnerDTO, PartnerVO>();
                config.CreateMap<SkillEquipVO, SkillEquipDTO>();
                config.CreateMap<SkillEquipDTO, SkillEquipVO>();
            });
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            string? connection = builder.Configuration.GetConnectionString("MySql");
            if (string.IsNullOrEmpty(connection))
                throw new NullReferenceException();
            AdClasses(builder);
            builder.Services.AddControllers();
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(3303);
            });
            builder.WebHost.UseKestrel();
            var app = builder.Build();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();

            app.Use(async (ctx, next) =>
            {
                Console.WriteLine("��û: " + ctx.Request.Path);
                await next();
            });
            app.MapControllers();
            //app.MapControllerRoute("default", "{controller=authorize}/{action=test}");
            app.MapHub<ChatHub>("/chat");
            app.Run();
        }

        private static void AdClasses(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IAuthorizeService, AuthorizeService>();
            builder.Services.AddTransient<IPlayerService, PlayerService>();
            builder.Services.AddSingleton<IPlayerManager, PlayerManager>();
            builder.Services.AddTransient<IPlayerChapterService, PlayerChapterService>();
            builder.Services.AddTransient<IPlayerGoodsService, PlayerGoodsService>();
            builder.Services.AddTransient<IPlayerStatService, PlayerStatService>();
            builder.Services.AddTransient<IPlayerSkillService, PlayerSkillService>();
            builder.Services.AddTransient<IPlayerPartnerService, PlayerPartnerService>();
        }

        private static void SetAuthorizeAndAuthentification(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication("UserKey")
                .AddCookie("UserKey", options =>
                {
                    options.Cookie.Name = "UserCookie";
                    options.LoginPath = "/authorize/log-in";
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    options.ExpireTimeSpan = TimeSpan.FromSeconds(3600 * 24 * 7);
                });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin",
                    policy => policy.RequireAssertion(context =>
                    context.User.IsInRole("Admin")));
                options.AddPolicy("User",
                    policy => policy.RequireAssertion(context =>
                    context.User.IsInRole("User")));
            });
        }
    }
}