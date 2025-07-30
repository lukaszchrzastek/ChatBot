using ChatBot.Application;
using ChatBot.Infrastructure;
using System.Text.Json.Serialization;

namespace ChatBot.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddHealthChecks();
			builder.Services.AddSingleton(TimeProvider.System);
			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});

			builder.Services.AddSignalR();
			builder.Services.AddInfrastructure(builder.Configuration);
			builder.Services.AddApplication();

			var app = builder.Build();
			app.ApplyMigrations();

			app.MapHealthEndpoints();
			app.ConfigureStaticAssets();
			app.UseUserIdentification();
			app.MapSignalR();

			app.UseAuthorization();
			app.MapControllers();

			app.Run();
		}
	}
}
