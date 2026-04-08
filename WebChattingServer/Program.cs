using BLL.Caching;
using BLL.Services.Authorizes;
using BLL.Services.Players.Application;
using BLL.Services.Players.Persistence;
using BLL.Services.Players.Persistence.Sections;
using BLL.Services.Players.Session;
using BLL.UoW;

namespace WebChattingServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSignalR();
            SetAuthorizeAndAuthentification(builder);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            AddServices(builder);
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
                Console.WriteLine("Request: " + ctx.Request.Path);
                await next();
            });

            app.MapControllers();
            app.Run();
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IPlayerManager, PlayerManager>();

            string connection = builder.Configuration.GetConnectionString("MySql")
                ?? throw new InvalidOperationException("MySql connection string is missing.");

            builder.Services.AddTransient<IUnitOfWorkFactory>(_ => new UnitOfWorkFactory(connection));
            builder.Services.AddTransient<IAuthorizeService, AuthorizeService>();
            builder.Services.AddTransient<IPlayerSessionService, PlayerSessionService>();
            builder.Services.AddTransient<IPlayerPersistenceService, PlayerPersistenceService>();
            builder.Services.AddTransient<IPlayerApplicationService, PlayerApplicationService>();

            builder.Services.AddTransient<IPlayerPersistenceSection, PlayerStatPersistenceSection>();
            builder.Services.AddTransient<IPlayerPersistenceSection, PlayerGoodsPersistenceSection>();
            builder.Services.AddTransient<IPlayerPersistenceSection, PlayerSkillPersistenceSection>();
            builder.Services.AddTransient<IPlayerPersistenceSection, PlayerChapterPersistenceSection>();
            builder.Services.AddTransient<IPlayerPersistenceSection, PlayerPartnerPersistenceSection>();
            builder.Services.AddTransient<IPlayerPersistenceSection, PlayerSkillEquipPersistenceSection>();
            builder.Services.AddTransient<IPlayerPersistenceSection, PlayerPartnerEquipPersistenceSection>();
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
                    options.ExpireTimeSpan = TimeSpan.FromDays(7);
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
