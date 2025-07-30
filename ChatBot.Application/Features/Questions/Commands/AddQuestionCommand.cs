using ChatBot.Domain.Models;
using MediatR;

namespace ChatBot.Application.Features.Questions.Commands
{
	public record AddQuestionCommand(Guid UserId, string QuestionText) : IRequest<Question>;
}
