namespace ChatBot.Application.Features.Questions.Queries
{
	using ChatBot.Domain.Models;
	using MediatR;

	public record GetUserQuery(Guid UserId) : IRequest<User?>;
}