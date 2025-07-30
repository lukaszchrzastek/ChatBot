using ChatBot.Application.Features.Questions.Commands;
using ChatBot.Domain.Models;
using ChatBot.Domain.Repositories;
using MediatR;

namespace ChatBot.Application.Features.Questions.Handlers
{
	public class CreateUserCommandHandler(
		IUserRepository userRepository,
		TimeProvider timeProvider
		) : IRequestHandler<CreateUserCommand, User>
	{
		public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var user = User.Create(Guid.NewGuid(), timeProvider.GetUtcNow().DateTime);

			await userRepository.AddAsync(user);

			return user;
		}
	}
}
