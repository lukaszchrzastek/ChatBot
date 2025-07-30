using ChatBot.Application.Features.Questions.Queries;
using ChatBot.Domain.Models;
using ChatBot.Domain.Repositories;
using MediatR;

namespace ChatBot.Application.Features.Questions.Handlers
{
	public class GetUserHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, User?>
	{
		private readonly IUserRepository _userRepository = userRepository;

		public async Task<User?> Handle(GetUserQuery request, CancellationToken cancellationToken)
		{
			return await _userRepository.GetUserAsync(request.UserId);
		}
	}
}