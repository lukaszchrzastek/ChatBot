using ChatBot.Application.Features.Questions.Queries;
using ChatBot.Domain.Models;
using ChatBot.Domain.Repositories;
using MediatR;

namespace ChatBot.Application.Features.Questions.Handlers
{
	public class GetQuestionsByUserIdHandler(IChatRepository chatRepository) : IRequestHandler<GetQuestionsByUserIdQuery, List<Question>>
	{
		private readonly IChatRepository _chatRepository = chatRepository;

		public async Task<List<Question>> Handle(GetQuestionsByUserIdQuery request, CancellationToken cancellationToken)
		{
			return await _chatRepository.GetUserQuestionsAsync(request.UserId);
		}
	}
}