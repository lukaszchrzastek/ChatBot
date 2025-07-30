using ChatBot.Domain.Models;
using ChatBot.Infrastructure.Persistence.Entities;

namespace ChatBot.Infrastructure.Persistence.Mappers
{
	public static class AnswerMapper
	{
		public static Answer MapFromEntity(AnswerEntity entity)
		{
			ArgumentNullException.ThrowIfNull(entity);

			var reaction = (Domain.ValueObjects.ReactionType?)(int?)entity.Reaction;

			return Answer.Create(entity.Id, entity.Text, entity.CreatedAt, reaction, entity.CanceledAt);

		}
	}
}
