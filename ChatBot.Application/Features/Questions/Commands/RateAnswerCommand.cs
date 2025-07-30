using ChatBot.Domain.ValueObjects;
using MediatR;

namespace ChatBot.Application.Features.Questions.Commands
{
	public record RateAnswerCommand(Guid QuestionId, Guid UserId, ReactionType Reaction) : IRequest<Unit>;
}
