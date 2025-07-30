using ChatBot.Application.Features.Questions.Commands;
using ChatBot.Domain.Repositories;
using MediatR;

namespace ChatBot.Application.Features.Questions.Handlers
{
	public class RateAnswerCommandHandler(IChatRepository chatRepository) : IRequestHandler<RateAnswerCommand, Unit>
	{
		public async Task<Unit> Handle(RateAnswerCommand request, CancellationToken cancellationToken)
		{
			var question = await chatRepository.GetQuestionByIdAsync(request.QuestionId);

			question.RateAnswer(request.Reaction);

			await chatRepository.UpdateQuestionAsync(question);

			return Unit.Value;
		}
	}
}