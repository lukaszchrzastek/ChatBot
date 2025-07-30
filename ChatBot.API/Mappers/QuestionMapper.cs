using ChatBot.API.Dtos;
using ChatBot.Domain.Models;

namespace ChatBot.API.Mappers
{
	public static class QuestionMapper
	{
		public static QuestionDto MapFromDomain(Question question)
		{
			var questionDto = new QuestionDto
			{
				Id = question.Id,
				Text = question.Text,
				CreatedAt = question.CreatedAt
			};

			if (question.Answer is not null)
			{
				questionDto.Answer = new AnswerDto
				{
					Id = question.Answer.Id,
					Text = question.Answer.Text,
					CreatedAt = question.Answer.CreatedAt,
					CanceledAt = question.Answer.CanceledAt,
					Reaction = (ReactionType?)(int?)question.Answer.Reaction
				};
			}

			return questionDto;
		}
	}
}