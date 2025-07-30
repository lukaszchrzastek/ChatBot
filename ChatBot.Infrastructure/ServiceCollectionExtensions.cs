using ChatBot.Domain.Repositories;
using ChatBot.Domain.Services;
using ChatBot.Infrastructure.Persistence;
using ChatBot.Infrastructure.Persistence.Repositories;
using ChatBot.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBot.Infrastructure
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddInfrastructure(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddSingleton<IChatWorker, ChatWorker>();
			services.AddScoped<IChatTaskRunner, ChatTaskRunner>();

			services.AddSingleton<IUserNotificationService, UserNotificationService>();
			services.AddSingleton<IAiChatService, FakeAiChatService>();
			services.AddScoped<IChatRepository, ChatRepository>();
			services.AddScoped<IUserRepository, UserRepository>();

			services.AddSingleton<IUserConnectionStore, UserConnectionStore>();

			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(
					configuration.GetConnectionString("DefaultConnection"),
					sqlOptions =>
					{
						sqlOptions.EnableRetryOnFailure();
					}
				)
			);

			return services;
		}
	}
}