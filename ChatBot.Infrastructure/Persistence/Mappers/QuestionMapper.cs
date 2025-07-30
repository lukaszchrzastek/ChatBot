using ChatBot.Domain.Models;
using ChatBot.Infrastructure.Persistence.Entities;

namespace ChatBot.Infrastructure.Persistence.Mappers
{
	public static class QuestionMapper
	{
		public static Question MapFromEntity(QuestionEntity questionEntity)
		{
			var question = Question.Create(questionEntity.Id, questionEntity.Text, questionEntity.CreatedAt);

			if (questionEntity.Answer != null)
				question.AddAnswer(AnswerMapper.MapFromEntity(questionEntity.Answer));

			return question;
		}
	}
}