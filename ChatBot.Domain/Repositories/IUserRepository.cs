using ChatBot.Domain.Models;

namespace ChatBot.Domain.Repositories
{
	public interface IUserRepository
	{
		Task<User?> GetUserAsync(Guid userId);
		Task AddAsync(User user);
	}
}