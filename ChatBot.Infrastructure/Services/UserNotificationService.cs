using ChatBot.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ChatBot.Infrastructure.Services
{
	public class UserNotificationService(
		IHubContext<ChatHub> hubContext,
		IUserConnectionStore userConnectionStore) : IUserNotificationService
	{

		public async Task SendPartialAnswerMessageAsync(Guid userId, PartialAnswerMessage partialAnswerMessage, CancellationToken cancellationToken)
		{
			var connectionIds = userConnectionStore.GetConnections(userId);
			foreach (var connectionId in connectionIds)
			{
				try
				{
					await hubContext.Clients.Client(connectionId).SendAsync("PartialAnswerMessage", partialAnswerMessage, cancellationToken);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"{connectionId}: {ex.Message}");
				}
			}
		}
	}
}
