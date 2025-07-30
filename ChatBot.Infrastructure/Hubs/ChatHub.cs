using ChatBot.Infrastructure.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatBot.Infrastructure.Hubs
{
	public class ChatHub(IUserConnectionStore userConnectionStore) : Hub
	{
		private Guid GetUserId()
		{
			var userId = Context.GetHttpContext().Items["UserId"]?.ToString();

			if (string.IsNullOrWhiteSpace(userId))
				return Guid.Empty;

			return Guid.TryParse(userId, out var parsedUserId) ? parsedUserId : Guid.Empty;
		}

		public override async Task OnConnectedAsync()
		{
			var userId = GetUserId();
			var connectionId = Context.ConnectionId;
			userConnectionStore.AddConnection(userId, connectionId);
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			var userId = GetUserId();
			var connectionId = Context.ConnectionId;

			userConnectionStore.RemoveConnection(userId, connectionId);

			await base.OnDisconnectedAsync(exception);
		}

		public async Task SendMessage(string user, string message)
		{
			await Clients.Caller.SendAsync("ReceiveMessage", user, message);
		}
	}
}