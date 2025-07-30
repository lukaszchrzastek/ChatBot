using ChatBot.Application.Features.Questions.Commands;
using ChatBot.Domain.Models;
using ChatBot.Domain.Repositories;
using ChatBot.Infrastructure.Services;
using MediatR;

namespace ChatBot.Application.Features.Questions.Handlers
{
	public class AddQuestionCommandHandler(
		IChatRepository repo,
		IChatWorker chatWorker,
		TimeProvider timeProvider
		) : IRequestHandler<AddQuestionCommand, Question>
	{
		public async Task<Question> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
		{
			var question = Question.Create(Guid.NewGuid(), request.QuestionText, timeProvider.GetUtcNow().DateTime);

			var questionId = await repo.AddQuestionAsync(request.UserId, question);

			chatWorker.AddTask(request.UserId, questionId, request.QuestionText);

			return question;
		}
	}
}