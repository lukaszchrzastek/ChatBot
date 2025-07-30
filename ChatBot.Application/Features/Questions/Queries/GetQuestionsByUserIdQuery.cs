namespace ChatBot.Application.Features.Questions.Queries
{
	using ChatBot.Domain.Models;
	using MediatR;

	public record GetQuestionsByUserIdQuery(Guid UserId) : IRequest<List<Question>>;
}