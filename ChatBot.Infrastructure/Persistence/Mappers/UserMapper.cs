using ChatBot.Domain.Models;
using ChatBot.Infrastructure.Persistence.Entities;

namespace ChatBot.Infrastructure.Persistence.Mappers
{
	public static class UserMapper
	{
		public static User MapFromEntity(UserEntity entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			return User.Create(entity.Id, entity.CreatedAt);
		}
	}
}