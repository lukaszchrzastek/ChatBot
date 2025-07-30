using System.Collections.Concurrent;

namespace ChatBot.Infrastructure.Services
{
	public class UserConnectionStore : IUserConnectionStore
	{
		private readonly ConcurrentDictionary<Guid, List<string>> _connections = new();

		public void AddConnection(Guid userId, string connectionId)
		{
			_connections.AddOrUpdate(userId,
				[connectionId],
				(_, list) =>
				{
					if (!list.Contains(connectionId))
						list.Add(connectionId);
					return list;
				});
		}

		public void RemoveConnection(Guid userId, string connectionId)
		{
			if (_connections.TryGetValue(userId, out var list))
			{
				list.Remove(connectionId);
				if (list.Count == 0)
					_connections.TryRemove(userId, out _);
			}
		}

		public List<string> GetConnections(Guid userId)
		{
			return _connections.TryGetValue(userId, out var list)
				? list
				: [];
		}
	}
}
