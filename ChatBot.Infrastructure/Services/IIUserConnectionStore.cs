namespace ChatBot.Infrastructure.Services
{
	public interface IUserConnectionStore
	{
		void AddConnection(Guid userId, string connectionId);
		void RemoveConnection(Guid userId, string connectionId);
		List<string> GetConnections(Guid userId);
	}
}
