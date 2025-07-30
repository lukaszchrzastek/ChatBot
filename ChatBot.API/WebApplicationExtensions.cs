using ChatBot.API.Middlewares;
using ChatBot.Infrastructure.Hubs;
using ChatBot.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.API
{
	public static class WebApplicationExtensions
	{
		public static void ApplyMigrations(this WebApplication app)
		{
			if (app.Environment.IsDevelopment())
			{
				using var scope = app.Services.CreateScope();
				var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
				dbContext.Database.Migrate();
			}
		}

		public static void MapHealthEndpoints(this WebApplication app)
		{
			app.MapHealthChecks("/health");
		}

		public static void ConfigureStaticAssets(this WebApplication app)
		{
			app.UseDefaultFiles();
			app.MapStaticAssets();
			app.MapFallbackToFile("/index.html");
		}

		public static void UseUserIdentification(this WebApplication app)
		{
			app.UseMiddleware<ExceptionMiddleware>();
			app.UseMiddleware<UserIdMiddleware>();
		}

		public static void MapSignalR(this WebApplication app)
		{
			app.MapHub<ChatHub>("/chathub");
		}
	}
}