using ChatBot.Domain.Models;
using MediatR;

namespace ChatBot.Application.Features.Questions.Commands
{
	public record CreateUserCommand : IRequest<User>;
}