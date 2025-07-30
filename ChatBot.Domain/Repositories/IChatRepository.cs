using ChatBot.Domain.Models;

namespace ChatBot.Domain.Repositories
{
	public interface IChatRepository
	{
		Task<Guid> AddQuestionAsync(Guid userId, Question question);

		Task<Question> GetQuestionByIdAsync(Guid questionId);

		Task<List<Question>> GetUserQuestionsAsync(Guid userId);

		Task UpdateQuestionAsync(Question question);
	}
}