using MediatR;

namespace ChatBot.Application.Features.Questions.Commands
{
	public record CancelQuestionCommand(Guid QuestionId, Guid UserId) : IRequest<Unit>;
}
