using ChatBot.Domain.Models;
using ChatBot.Domain.Repositories;

using ChatBot.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Infrastructure.Persistence.Repositories
{
	public class UserRepository(AppDbContext context) : IUserRepository
	{
		private readonly AppDbContext _context = context;

		public async Task<User?> GetUserAsync(Guid userId)
		{
			var userEntity = await _context.Users
			.AsNoTracking()
			.SingleOrDefaultAsync(u => u.Id == userId);
			return userEntity == null ? null : Mappers.UserMapper.MapFromEntity(userEntity);
		}

		public async Task AddAsync(User user)
		{
			var userEntity = new UserEntity
			{
				Id = user.Id,
				CreatedAt = user.CreatedAt
			};

			_context.Users.Add(userEntity);
			await _context.SaveChangesAsync();
		}
	}
}
