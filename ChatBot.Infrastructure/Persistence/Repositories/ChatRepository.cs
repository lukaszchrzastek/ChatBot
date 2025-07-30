using ChatBot.Domain.Models;
using ChatBot.Domain.Repositories;

using ChatBot.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Infrastructure.Persistence.Repositories
{
	public class ChatRepository(AppDbContext context) : IChatRepository
	{
		private readonly AppDbContext _context = context;

		public async Task<Guid> AddQuestionAsync(Guid userId, Question question)
		{
			var questionEntity = new QuestionEntity
			{
				UserEntityId = userId,
				Id = question.Id,
				Text = question.Text,
				CreatedAt = question.CreatedAt
			};

			_context.Questions.Add(questionEntity);
			await _context.SaveChangesAsync();

			return questionEntity.Id;
		}

		public async Task<List<Question>> GetUserQuestionsAsync(Guid userId)
		{
			var entities = await _context.Questions
			.Where(x => x.UserEntityId == userId)
			.Include(x => x.Answer)
			.ToListAsync();

			return [.. entities.Select(Mappers.QuestionMapper.MapFromEntity)];
		}

		public async Task<Question> GetQuestionByIdAsync(Guid questionId)
		{
			var question = await _context.Questions
				.Include(q => q.Answer)
				.FirstOrDefaultAsync(q => q.Id == questionId);
			return question == null
				? throw new InvalidOperationException("Pytanie nie istnieje")
				: Mappers.QuestionMapper.MapFromEntity(question);
		}

		public async Task UpdateQuestionAsync(Question question)
		{
			var questionEntity = await _context.Questions
				.Include(q => q.Answer)
				.FirstOrDefaultAsync(q => q.Id == question.Id) ?? throw new InvalidOperationException("Pytanie nie istnieje");
			questionEntity.Text = question.Text;
			questionEntity.CreatedAt = question.CreatedAt;


			if (question.Answer != null)
			{
				if (questionEntity.Answer == null)
				{
					questionEntity.Answer = new AnswerEntity
					{
						Id = question.Answer.Id,
						Text = question.Answer.Text,
						CreatedAt = question.Answer.CreatedAt,
						CanceledAt = question.Answer.CanceledAt,
						Reaction = (ReactionType?)(int?)question.Answer.Reaction
					};
					_context.Entry(questionEntity.Answer).State = EntityState.Added;
				}
				else
				{
					questionEntity.Answer.Text = question.Answer.Text;
					questionEntity.Answer.Reaction = (ReactionType?)(int?)question.Answer.Reaction;
					questionEntity.Answer.CanceledAt = question.Answer.CanceledAt;
					_context.Entry(questionEntity.Answer).State = EntityState.Modified;
				}
			}
			await _context.SaveChangesAsync();
		}
	}
}